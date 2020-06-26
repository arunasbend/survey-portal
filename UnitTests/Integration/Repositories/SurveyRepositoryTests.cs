using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SurveyPortal.Data;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Dto;
using UnitTests.Integration.DataSets;

namespace UnitTests.Integration.Repositories
{
    [TestFixture]
    public class SurveyRepositoryTests: Base
    {
        [SetUp]
        public void SetUp()
        {
            UsingDatabase(context =>
            {
                context.Answers.AddRange(AnswersDataSet.Data());
                context.Questions.AddRange(QuestionDataSet.Data());
                context.Surveys.AddRange(SurveyDataSet.Data());
                context.SaveChanges();
            });
        }

        [Test]
        public void Survey_ShouldGetById()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveyRepository(context);

                var data = repo.GetById(Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"));

                var radioQuestion =
                    data.Questions.First(x => x.Type == QuestionType.radio);

                Assert.That(data.Title, Is.EqualTo("Survey1"));
                Assert.That(data.Description, Is.EqualTo("Description"));
                Assert.That(data.Status, Is.EqualTo(Status.Published));
                Assert.That(data.CreatedOn, Is.EqualTo(new DateTime(2020,1,1,12,0,0)));
                Assert.That(data.ModifiedOn, Is.EqualTo(new DateTime(2020,1,2,12,0,0)));
                Assert.That(data.DueDate, Is.EqualTo(new DateTimeOffset(new DateTime(2020,2,1,12,0,0))));
                Assert.That(data.Questions.Count, Is.EqualTo(4));
                Assert.That(radioQuestion.Answers.Count, Is.EqualTo(3));
                
                //Assert.That(data.UserGroups.Count, Is.EqualTo(2));
                //Assert.That(data.UserGroups.First(x => x.Title == "Admins").Users.Count, Is.EqualTo(1));
                //Assert.That(data.UserGroups.First(x => x.Title == "Users").Users.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void ShouldFormACorrectDtoForUpdateView()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveyRepository(context);

                var data = repo.GetForUpdateById(Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"));

                Assert.That(data.Title, Is.EqualTo("Survey1"));
                Assert.That(data.Description, Is.EqualTo("Description"));
                Assert.That(data.Date, Is.EqualTo(new DateTimeOffset(new DateTime(2020,2,1,12,0,0))));
                Assert.That(data.Questions.Count, Is.EqualTo(4));

                var radioQuestion = data.Questions.First(x => x.Type == "radio");
                Assert.That(radioQuestion.Title, Is.EqualTo("What is the best color"));
                Assert.That(radioQuestion.Description, Is.EqualTo("Describe the best color"));
                Assert.That(radioQuestion.Radio.Count, Is.EqualTo(3));
                Assert.That(radioQuestion.Radio.FirstOrDefault(x => x == "Radio1"), Is.Not.Null);
                Assert.That(radioQuestion.Radio.FirstOrDefault(x => x == "Radio2"), Is.Not.Null);
                Assert.That(radioQuestion.Radio.FirstOrDefault(x => x == "Radio3"), Is.Not.Null);

                var checkQuestion = data.Questions.First(x => x.Type == "check");
                Assert.That(checkQuestion.Title, Is.EqualTo("What colors match up the best"));
                Assert.That(checkQuestion.Description, Is.EqualTo("Match some colors"));
                Assert.That(checkQuestion.Check.Count, Is.EqualTo(3));
                Assert.That(checkQuestion.Check.FirstOrDefault(x => x == "Check1"), Is.Not.Null);
                Assert.That(checkQuestion.Check.FirstOrDefault(x => x == "Check2"), Is.Not.Null);
                Assert.That(checkQuestion.Check.FirstOrDefault(x => x == "Check3"), Is.Not.Null);

                var textAreaQuestion = data.Questions.First(x => x.Type == "text");
                Assert.That(textAreaQuestion.Title, Is.EqualTo("What do you know about color"));
                Assert.That(textAreaQuestion.Description, Is.EqualTo("Expand up a little bit"));
                Assert.That(textAreaQuestion.Textarea.Text, Is.EqualTo("TextArea answer"));
                
                var rangeQuestion = data.Questions.First(x => x.Type == "range");
                Assert.That(rangeQuestion.Title, Is.EqualTo("How much do you like drawing"));
                Assert.That(rangeQuestion.Description, Is.EqualTo("Rate it"));
                Assert.That(rangeQuestion.Range.Min, Is.EqualTo("1"));
                Assert.That(rangeQuestion.Range.Max, Is.EqualTo("10"));
                Assert.That(rangeQuestion.Range.Range, Is.EqualTo("5"));
            });
        }
        
        [Test]
        public void QuestionMustHaveAnswers()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveyRepository(context);

                var question = context.Questions.First(x => x.Type == QuestionType.check);
                question.Answers = new List<QuestionAnswer>();
                var answers = context.QuestionAnswers.Where(x => x.QuestionId == question.Id).ToList();
                context.RemoveRange(answers);
                context.SaveChanges();

                Assert.Throws<NotSupportedException>(() =>
                    repo.GetForUpdateById(Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4")));
            });
        }

        [Test]
        public void RangeQuestionShouldHaveThreeParts()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveyRepository(context);

                var question = context.Questions.First(x => x.Type == QuestionType.range);
                var answers = context.QuestionAnswers.First(x => x.QuestionId == question.Id);
                answers.Answer = "5";
                context.SaveChanges();

                Assert.Throws<NotSupportedException>(() =>
                    repo.GetForUpdateById(Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4")));

                answers.Answer = "1|6|10";
                context.SaveChanges();

                var dto = repo.GetForUpdateById(Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"));

                var rangeQuestion = dto.Questions.First(x => x.Type == "range");

                Assert.That(rangeQuestion.Range.Min, Is.EqualTo("1"));
                Assert.That(rangeQuestion.Range.Max, Is.EqualTo("10"));
                Assert.That(rangeQuestion.Range.Range, Is.EqualTo("6"));
            });
        }

        [Test]
        public void SurveyShouldCreateSuccessfully()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveyRepository(context);

                var offset = DateTimeOffset.Now;

                var dto = repo.Create(new SurveyDto
                {
                    Title = "New survey",
                    Date = offset,
                    Description = "New survey description",
                    UserGroups = new List<UserGroupDto>(),
                    Questions = new List<QuestionDto>
                    {
                        new QuestionDto
                        {
                            Title = "Q1",
                            Description = "D1",
                            Type = "text",
                            Mandatory = false,
                            Textarea = new TextareaDto
                            {
                                Text = "Text placeholder"
                            },
                        },
                        new QuestionDto
                        {
                            Title = "Q2",
                            Description = "D2",
                            Type = "radio",
                            Mandatory = true,
                            Radio = new List<string>
                            {
                                "RadioOption1", "RadioOption2"
                            }
                        },
                        new QuestionDto
                        {
                            Title = "Q3",
                            Description = "D3",
                            Type = "check",
                            Mandatory = false,
                            Check = new List<string>
                            {
                                "CheckOption1", "CheckOption2"
                            }
                        },
                        new QuestionDto
                        {
                            Title = "Q4",
                            Description = "D4",
                            Type = "range",
                            Mandatory = false,
                            Range = new RangeDto
                            {
                                Min = "1",
                                Range = "2",
                                Max = "5",
                            }
                        }
                    }
                });

                var newSurvey = context.Surveys.First(x => x.Id == dto.Id);

                Assert.That(newSurvey.Title, Is.EqualTo("New survey"));
                Assert.That(newSurvey.Description, Is.EqualTo("New survey description"));
                Assert.That(newSurvey.DueDate, Is.EqualTo(offset));

                var textQuestion = newSurvey.Questions.First(x => x.Type == QuestionType.text);
                Assert.That(textQuestion.Title, Is.EqualTo("Q1"));
                Assert.That(textQuestion.Description, Is.EqualTo("D1"));
                Assert.That(textQuestion.Mandatory, Is.EqualTo(false));
                Assert.That(textQuestion.Answers.First().Answer, Is.EqualTo("Text placeholder"));
                
                var radioQuestion = newSurvey.Questions.First(x => x.Type == QuestionType.radio);
                Assert.That(radioQuestion.Title, Is.EqualTo("Q2"));
                Assert.That(radioQuestion.Description, Is.EqualTo("D2"));
                Assert.That(radioQuestion.Mandatory, Is.EqualTo(true));
                Assert.That(radioQuestion.Answers.First(x => x.Answer == "RadioOption1"), Is.Not.Null);
                Assert.That(radioQuestion.Answers.First(x => x.Answer == "RadioOption2"), Is.Not.Null);

                var checkQuestion = newSurvey.Questions.First(x => x.Type == QuestionType.check);
                Assert.That(checkQuestion.Title, Is.EqualTo("Q3"));
                Assert.That(checkQuestion.Description, Is.EqualTo("D3"));
                Assert.That(checkQuestion.Mandatory, Is.EqualTo(false));
                Assert.That(checkQuestion.Answers.First(x => x.Answer == "CheckOption1"), Is.Not.Null);
                Assert.That(checkQuestion.Answers.First(x => x.Answer == "CheckOption2"), Is.Not.Null);

                var rangeQuestion = newSurvey.Questions.First(x => x.Type == QuestionType.range);
                Assert.That(rangeQuestion.Title, Is.EqualTo("Q4"));
                Assert.That(rangeQuestion.Description, Is.EqualTo("D4"));
                Assert.That(rangeQuestion.Mandatory, Is.EqualTo(false));
                Assert.That(rangeQuestion.Answers.First().Answer, Is.EqualTo("1|2|5"));
            });
        }

        [Test]
        public void TextQuestionShouldThrowIfArgsAreNotRight()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveyRepository(context);

                var offset = DateTimeOffset.Now;

                var error = Assert.Throws<ArgumentNullException>(() =>
                {
                    repo.Create(new SurveyDto
                    {
                        Title = "New survey",
                        Date = offset,
                        Description = "New survey description",
                        UserGroups = new List<UserGroupDto>(),
                        Questions = new List<QuestionDto>
                        {
                            new QuestionDto
                            {
                                Title = "Q1",
                                Description = "D1",
                                Type = "text",
                                Mandatory = false,
                            },
                        },
                    });
                });

                Assert.That(error.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: Textarea"));
            });
        }

        [Test]
        public void RangeQuestionShouldThrowIfArgsAreNotRight()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveyRepository(context);

                var offset = DateTimeOffset.Now;

                var error = Assert.Throws<ArgumentNullException>(() =>
                {
                    repo.Create(new SurveyDto
                    {
                        Title = "New survey",
                        Date = offset,
                        Description = "New survey description",
                        UserGroups = new List<UserGroupDto>(),
                        Questions = new List<QuestionDto>
                        {
                            new QuestionDto
                            {
                                Title = "Q1",
                                Description = "D1",
                                Type = "range",
                                Mandatory = false,
                            },
                        },
                    });
                });

                Assert.That(error.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: Range"));
            });
        }

        [Test]
        public void CheckQuestionShouldThrowIfArgsAreNotRight()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveyRepository(context);

                var offset = DateTimeOffset.Now;

                var error = Assert.Throws<ArgumentNullException>(() =>
                {
                    repo.Create(new SurveyDto
                    {
                        Title = "New survey",
                        Date = offset,
                        Description = "New survey description",
                        UserGroups = new List<UserGroupDto>(),
                        Questions = new List<QuestionDto>
                        {
                            new QuestionDto
                            {
                                Title = "Q1",
                                Description = "D1",
                                Type = "check",
                                Mandatory = false,
                            },
                        },
                    });
                });

                Assert.That(error.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: Check"));
            });
        }

        [Test]
        public void RadioQuestionShouldThrowIfArgsAreNotRight()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveyRepository(context);

                var offset = DateTimeOffset.Now;

                var error = Assert.Throws<ArgumentNullException>(() =>
                {
                    repo.Create(new SurveyDto
                    {
                        Title = "New survey",
                        Date = offset,
                        Description = "New survey description",
                        UserGroups = new List<UserGroupDto>(),
                        Questions = new List<QuestionDto>
                        {
                            new QuestionDto
                            {
                                Title = "Q1",
                                Description = "D1",
                                Type = "radio",
                                Mandatory = false,
                            },
                        },
                    });
                });

                Assert.That(error.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: Radio"));
            });
        }

        [Test]
        public void SurveyCreationShouldStopIfTypeIsNotRecognized()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveyRepository(context);

                var offset = DateTimeOffset.Now;

                var error = Assert.Throws<NotSupportedException>(() =>
                {
                    repo.Create(new SurveyDto
                    {
                        Title = "New survey",
                        Date = offset,
                        Description = "New survey description",
                        UserGroups = new List<UserGroupDto>(),
                        Questions = new List<QuestionDto>
                        {
                            new QuestionDto
                            {
                                Title = "Q1",
                                Description = "D1",
                                Type = "RANDOMTYPE",
                                Mandatory = false,
                            },
                        },
                    });
                });

                Assert.That(error.Message, Is.EqualTo("Unrecognized question type RANDOMTYPE"));
            });
        }

        [Test]
        public void SurveyShouldUpdateSuccessfully()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveyRepository(context);

                var offset = DateTimeOffset.Now;

                repo.Update(Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"), new SurveyDto
                {
                    Title = "New survey",
                    Date = offset,
                    Description = "New survey description",
                    UserGroups = new List<UserGroupDto>
                    {
                        new UserGroupDto
                        {
                            Id = Guid.Parse("98d5f60b-f88f-4a56-9827-559b71d1c83c"),
                            Title = "Admins",
                            Selected = true,
                        }
                    },
                    Questions = new List<QuestionDto>
                    {
                        new QuestionDto
                        {
                            Title = "Q1",
                            Description = "D1",
                            Type = "text",
                            Mandatory = false,
                            Textarea = new TextareaDto
                            {
                                Text = "Text placeholder"
                            },
                        },
                        new QuestionDto
                        {
                            Title = "Q2",
                            Description = "D2",
                            Type = "radio",
                            Mandatory = true,
                            Radio = new List<string>
                            {
                                "RadioOption1", "RadioOption2"
                            }
                        },
                        new QuestionDto
                        {
                            Title = "Q3",
                            Description = "D3",
                            Type = "check",
                            Mandatory = false,
                            Check = new List<string>
                            {
                                "CheckOption1", "CheckOption2"
                            }
                        },
                        new QuestionDto
                        {
                            Title = "Q4",
                            Description = "D4",
                            Type = "range",
                            Mandatory = false,
                            Range = new RangeDto
                            {
                                Min = "1",
                                Range = "2",
                                Max = "5",
                            }
                        }
                    }
                });

                var newSurvey = context.Surveys.First(x => x.Id == Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"));

                Assert.That(newSurvey.Title, Is.EqualTo("New survey"));
                Assert.That(newSurvey.Description, Is.EqualTo("New survey description"));
                Assert.That(newSurvey.DueDate, Is.EqualTo(offset));

                Assert.That(newSurvey.UserGroups.Count, Is.EqualTo(1));
                Assert.That(newSurvey.UserGroups.First().Title, Is.EqualTo("Admins"));
                //Assert.That(newSurvey.UserGroups.First().Users.Count, Is.EqualTo(1));

                var textQuestion = newSurvey.Questions.First(x => x.Type == QuestionType.text);
                Assert.That(textQuestion.Title, Is.EqualTo("Q1"));
                Assert.That(textQuestion.Description, Is.EqualTo("D1"));
                Assert.That(textQuestion.Mandatory, Is.EqualTo(false));
                Assert.That(textQuestion.Answers.First().Answer, Is.EqualTo("Text placeholder"));
                
                var radioQuestion = newSurvey.Questions.First(x => x.Type == QuestionType.radio);
                Assert.That(radioQuestion.Title, Is.EqualTo("Q2"));
                Assert.That(radioQuestion.Description, Is.EqualTo("D2"));
                Assert.That(radioQuestion.Mandatory, Is.EqualTo(true));
                Assert.That(radioQuestion.Answers.First(x => x.Answer == "RadioOption1"), Is.Not.Null);
                Assert.That(radioQuestion.Answers.First(x => x.Answer == "RadioOption2"), Is.Not.Null);

                var checkQuestion = newSurvey.Questions.First(x => x.Type == QuestionType.check);
                Assert.That(checkQuestion.Title, Is.EqualTo("Q3"));
                Assert.That(checkQuestion.Description, Is.EqualTo("D3"));
                Assert.That(checkQuestion.Mandatory, Is.EqualTo(false));
                Assert.That(checkQuestion.Answers.First(x => x.Answer == "CheckOption1"), Is.Not.Null);
                Assert.That(checkQuestion.Answers.First(x => x.Answer == "CheckOption2"), Is.Not.Null);

                var rangeQuestion = newSurvey.Questions.First(x => x.Type == QuestionType.range);
                Assert.That(rangeQuestion.Title, Is.EqualTo("Q4"));
                Assert.That(rangeQuestion.Description, Is.EqualTo("D4"));
                Assert.That(rangeQuestion.Mandatory, Is.EqualTo(false));
                Assert.That(rangeQuestion.Answers.First().Answer, Is.EqualTo("1|2|5"));
            });
        }

        [Test]
        public void ShoudGetOverview()
        {
            UsingDatabase(context =>
            {
                context.Surveys.Add(new Survey
                {
                    Id = Guid.NewGuid(),
                    Title = "Completed1",
                    Status = Status.Completed,
                });
                                
                context.Surveys.Add(new Survey
                {
                    Id = Guid.NewGuid(),
                    Title = "Completed2",
                    Status = Status.Completed,
                });

                context.Surveys.Add(new Survey
                {
                    Id = Guid.NewGuid(),
                    Title = "Disabled",
                    Status = Status.Disabled,
                });

                context.SaveChanges();

                var repo = new SurveyRepository(context);

                var result = repo.GetSurveyOverview();

                Assert.That(result.Header.SurveyData.First(x => x.Title == "Published").Count, Is.EqualTo(1));
                Assert.That(result.Header.SurveyData.First(x => x.Title == "Completed").Count, Is.EqualTo(2));
                Assert.That(result.Header.SurveyData.First(x => x.Title == "Disabled").Count, Is.EqualTo(1));
                
                Assert.That(result.Header.StatisticsData.First(x => x.Title == "In data base").Count, Is.EqualTo(4));

                Assert.That(result.SurveyManager.First(x => x.Name == "Survey1").Status, Is.EqualTo("Published"));
                Assert.That(result.SurveyManager.First(x => x.Name == "Disabled").Status, Is.EqualTo("Disabled"));
                Assert.That(result.SurveyManager.First(x => x.Name == "Completed1").Status, Is.EqualTo("Completed"));
                Assert.That(result.SurveyManager.First(x => x.Name == "Completed2").Status, Is.EqualTo("Completed"));
            });
        }
    }
}
