using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Requests;
using SurveyPortal.DataContracts.Responses;

namespace SurveyPortal.Data
{
    public interface ISurveySubmissionRepository: IGenericCrudRepository<SurveySubmission, object, CreateSurveySubmissionRequest>
    {
        List<SurveySubmission> GetBySurveyId(Guid id);
        SurveySubmission Create(CreateSurveySubmissionRequest request, string userId);
        List<SurveyResultsQuestion> GetSurveyQuestionResults(Survey survey, UserManager<User> userManager);
    }

    public class SurveySubmissionRepository : GenericCrudRepository<SurveySubmission, object, CreateSurveySubmissionRequest>, ISurveySubmissionRepository
    {
        private readonly AppDbContext _dbContext;

        public SurveySubmissionRepository(AppDbContext dbContext) : base(dbContext, dbContext.SurveySubmissions)
        {
            _dbContext = dbContext;
        }

        public List<SurveySubmission> GetBySurveyId(Guid id)
        {
            return _dbContext.SurveySubmissions.Where(x => x.SurveyId == id).ToList();
        }

        // TODO: User should be able to answer once per survey
        public SurveySubmission Create(CreateSurveySubmissionRequest request, string userId)
        {
            var surveySubmissionId = Guid.NewGuid();
            var item = new SurveySubmission
            {
                Id = surveySubmissionId,
                SurveyId = request.Id,
                UserId = userId,
            };
            _dbContext.SurveySubmissions.Add(item);

            foreach (var question in request.questions)
            {
                var answer = new Answer();

                var answerId = Guid.NewGuid();
                answer.Id = answerId;
                answer.SurveySubmissionId = surveySubmissionId;
                answer.QuestionId = question.id;
                
                if (question.type == (int)QuestionType.radio || question.type == (int)QuestionType.check)
                {
                    var answersdata = JsonConvert.DeserializeObject<List<AnswerData>>(question.data.ToString());

                    var selectedAnswers = answersdata.Select(x => new SelectedAnswer
                        {
                            QuestionAnswerId = x.Id,
                            AnswerId = answerId,
                            Selected = x.Checked.ToString(),
                        })
                        .ToList();
                    answer.Selected = selectedAnswers;

                    _dbContext.SelectedAnswers.AddRange(selectedAnswers);
                }
                else if (question.type == (int)QuestionType.text || question.type == (int)QuestionType.range)
                {
                    answer.Text = question.data.ToString();
                }

                AnswerRepository answerRepository = new AnswerRepository(_dbContext);
                answerRepository.Create(answer);
            }

            _dbContext.SaveChanges();

            return item;
        }

        public List<SurveyResultsQuestion> GetSurveyQuestionResults(Survey survey, UserManager<User> userManager)
        {
            return survey.Questions.Select(question => new SurveyResultsQuestion
                {
                    Id = question.Id,
                    Title = question.Title,
                    Type = question.Type.ToString(),
                    Rows = FormatQuestionResults(userManager, question)
                })
                .ToList();
        }

        private List<SurveyResultsRow> FormatQuestionResults(UserManager<User> userManager, Question question)
        {
            List<SurveyResultsRow> rows = new List<SurveyResultsRow>();

            switch (question.Type)
            {
                case QuestionType.check:
                case QuestionType.radio:
                    _dbContext.QuestionAnswers
                        .Where(x => x.QuestionId == question.Id)
                        .ToList()
                        .ForEach(x =>
                        {
                            var selections = _dbContext.SelectedAnswers
                                .Where(y => y.QuestionAnswerId == x.Id)
                                .ToList();

                            RadioAndCheckSubmissionResult(x, rows, selections);
                        });
                    break;
                case QuestionType.text:
                    _dbContext.Answers
                        .Include(a => a.SurveySubmission)
                        .Where(x => x.QuestionId == question.Id)
                        .ToList()
                        .ForEach(x =>
                        {
                            TextSubmissionResult(userManager, x, rows);
                        });
                    break;
                case QuestionType.range:
                    var answers = _dbContext.Answers
                        .Where(x => x.QuestionId == question.Id)
                        .ToList();
                    RangeSubmissionResult(rows, answers);
                    break;
            }
            return rows;
        }

        private static void RangeSubmissionResult(List<SurveyResultsRow> rows, List<Answer> answers)
        {
            var average = answers.Sum(x => int.Parse(x.Text)) * 10 / answers.Count;
            
            rows.Add(new SurveyResultsRow
            {
                Id = Guid.NewGuid(),
                Amount = average,
            });
        }

        private static void TextSubmissionResult(UserManager<User> userManager, Answer x, List<SurveyResultsRow> rows)
        {
            var user = userManager.FindByIdAsync(x.SurveySubmission.UserId).Result.UserName;
            
            rows.Add(new SurveyResultsRow
            {
                Id = Guid.NewGuid(),
                UserName = user,
                TextArea = x.Text,
            });
        }

        private static void RadioAndCheckSubmissionResult(QuestionAnswer x, List<SurveyResultsRow> rows, List<SelectedAnswer> selections)
        {
            var avg = selections.Count(y => bool.Parse(y.Selected)) * 100 / selections.Count;

            rows.Add(new SurveyResultsRow
            {
                Id = Guid.NewGuid(),
                Label = x.Answer,
                Amount = avg,
            });
        }

        //public new void Update(Guid id, UpdateSurveySubmissionRequest request)
        //{
        //    var item = _dbContext.SurveySubmissions.Single(x => x.Id == id);

        //    item.Answers = request.Answers;

        //    _dbContext.SaveChanges();
        //}
        //public class UpdateSurveySubmissionRequest
        //{
        //    [Required]
        //    public List<Answer> Answers { get; set; }
        //}
    }
}
