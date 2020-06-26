using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Dto;
using SurveyPortal.DataContracts.Requests;
using SurveyPortal.DataContracts.Responses;
using SurveyPortal.Services;

namespace SurveyPortal.Data
{
    public interface ISurveyRepository: IGenericCrudRepository<Survey, SurveyDto, SurveyDto>
    {
        SurveyDto GetForUpdateById(Guid id);
        SurveyOverviewResponse GetSurveyOverview();
    }

    public class SurveyRepository : GenericCrudRepository<Survey, SurveyDto, SurveyDto>, ISurveyRepository
    {
        private readonly AppDbContext _dbContext;

        public SurveyRepository(AppDbContext dbContext) : base(dbContext, dbContext.Surveys)
        {
            _dbContext = dbContext;
        }

        public new Survey GetById(Guid id)
        {
            return _dbContext.Surveys
                .Include(s => s.Questions)
                .ThenInclude(q => q.Answers)
                .SingleOrDefault(s => s.Id == id);
        }

        public SurveyDto GetForUpdateById(Guid id)
        {
            var survey = GetById(id);
            var surveyDto = new SurveyDto
            {
                Title = survey.Title,
                Date = survey.DueDate,
                Description = survey.Description
            };

            var questionsDto = new List<QuestionDto>();
            foreach (var question in survey.Questions)
            {
                var questionDto = new QuestionDto
                {
                    Type = Enum.GetName(typeof(QuestionType), question.Type),
                    Title = question.Title,
                    Check = new List<string> {null},
                    Radio = new List<string> {null},
                    Description = question.Description,
                    Mandatory = question.Mandatory
                };

                if (QuestionType.check.Equals(question.Type) && question.Answers.HasElements())
                {
                    questionDto.Check = question.Answers.Select(x => x.Answer).ToList();
                }
                else if (QuestionType.radio.Equals(question.Type) && question.Answers.HasElements())
                {
                    questionDto.Radio = question.Answers.Select(x => x.Answer).ToList();
                }
                else if (QuestionType.text.Equals(question.Type) && question.Answers.HasElements())
                {
                    questionDto.Textarea = new TextareaDto
                    {
                        Text = question.Answers.Select(x => x.Answer).FirstOrDefault()
                    };
                }
                else if (QuestionType.range.Equals(question.Type) && question.Answers.HasElements())
                {
                    var answers = question.Answers.Select(x => x.Answer).First();

                    var answer = answers.Split('|');

                    if (answer.HasExactElements(3))
                    {
                        questionDto.Range = new RangeDto
                        {
                            Min = answer[0],
                            Range = answer[1],
                            Max = answer[2],
                        };
                    }
                    else
                    {
                        throw new NotSupportedException($"Cannot parse the range. {answer} is invalid.");
                    }
                }
                else
                {
                    throw new NotSupportedException("Question must have answers");
                }

                questionsDto.Add(questionDto);
            }

            surveyDto.Questions = questionsDto;
            
            return surveyDto;
        }

        public new Survey Create(SurveyDto dto)
        {
            QuestionRepository questionRepository = new QuestionRepository(_dbContext);

            Guid surveyId = Guid.NewGuid();
            
            var selectedUserGroups = dto.UserGroups
                                               .Where(x => x.Selected)
                                               .Select(x => x.Id)
                                               .ToList();

            var userGroups = _dbContext.UserGroups.Where(x => selectedUserGroups.Contains(x.Id)).ToList();

            var surveyItem = new Survey
            {
                Title = dto.Title,
                DueDate = dto.Date,
                CreatedOn = DateTime.Now,
                Id = surveyId,
                Description = dto.Description,
                Status = Status.Published,
                UserGroups = userGroups,
            };

            foreach (var question in dto.Questions)
            {
                SaveQuestionFromDto(questionRepository, surveyId, question);
            }

            _dbContext.Surveys.Add(surveyItem);
            _dbContext.SaveChanges();

            return surveyItem;
        }

        public new void Update(Guid id, SurveyDto dto)
        {
            var survey = _dbContext.Surveys.Include(s => s.Questions).Include(s => s.UserGroups).Single(x => x.Id == id);
            _dbContext.Questions.RemoveRange(survey.Questions);

            QuestionRepository questionRepository = new QuestionRepository(_dbContext);
            var selectedUserGroups = dto.UserGroups
                                               .Where(x => x.Selected)
                                               .Select(x => x.Id)
                                               .ToList();
            survey.Title = dto.Title;
            survey.DueDate = dto.Date;
            survey.ModifiedOn = DateTime.Now;
            survey.Description = dto.Description;
            survey.UserGroups.Clear();
            survey.UserGroups.AddRange(_dbContext.UserGroups.Where(x => selectedUserGroups.Contains(x.Id)).ToList());

            foreach (var question in dto.Questions)
            {
                SaveQuestionFromDto(questionRepository, survey.Id, question);
            }

            _dbContext.SaveChanges();
        }

        public SurveyOverviewResponse GetSurveyOverview()
        {
            var response = new SurveyOverviewResponse
            {
                Header = new SurveyOverviewResponseHeader
                {
                    SurveyData = new List<SurveyData>
                    {
                        new SurveyData
                        {
                            Title = "Published",
                            Count = _dbContext.Surveys.Count(x => x.Status == Status.Published)
                        },
                        new SurveyData
                        {
                            Title = "Disabled",
                            Count = _dbContext.Surveys.Count(x => x.Status == Status.Disabled)
                        },
                        new SurveyData
                        {
                            Title = "Completed",
                            Count = _dbContext.Surveys.Count(x => x.Status == Status.Completed)
                        }
                    },
                    StatisticsData = new List<StatisticsData>
                    {
                        new StatisticsData
                        {
                            Title = "In data base",
                            Count = _dbContext.Surveys.Count()
                        },
                    }
                },
                SurveyManager = _dbContext.Surveys
                    .Select(x => new SurveyManager
                    {
                        Id = x.Id,
                        Name = x.Title,
                        Status = x.Status.ToString()
                    })
                    .ToList()
            };

            return response;
        }

        private static void SaveQuestionFromDto(QuestionRepository questionRepository, Guid surveyId, QuestionDto question)
        {
            var questionItem = new CreateQuestionRequest
            {
                SurveyId = surveyId,
                Answers = SaveQuestionByType(question),
                Type = (QuestionType)Enum.Parse(typeof(QuestionType), question.Type, true),
                Id = Guid.NewGuid(),
                Title = question.Title,
                Description = question.Description,
                Mandatory = question.Mandatory,
            };

            questionRepository.Create(questionItem);
        }

        private static List<string> SaveQuestionByType(QuestionDto question)
        {
            List<string> answers;

            if (question.Type.Equals("range"))
            {
                if (question.Range == null)
                {
                    throw new ArgumentNullException("Range");
                }

                answers = new List<string> { question.Range.Min + "|" + question.Range.Range + "|" + question.Range.Max };
            }
            else if (question.Type.Equals("text"))
            {
                if (question.Textarea == null)
                {
                    throw new ArgumentNullException("Textarea");
                }

                answers = new List<string> { question.Textarea.Text };
            }
            else if (question.Type.Equals("check"))
            {
                if (question.Check == null)
                {
                    throw new ArgumentNullException("Check");
                }

                answers = question.Check;
            }
            else if (question.Type.Equals("radio"))
            {
                if (question.Radio == null)
                {
                    throw new ArgumentNullException("Radio");
                }

                answers = question.Radio;
            }
            else
            {
                throw new NotSupportedException($"Unrecognized question type {question.Type}");
            }

            return answers;
        }
    }
}
