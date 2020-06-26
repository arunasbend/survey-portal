using NUnit.Framework;
using SurveyPortal.Data;
using SurveyPortal.DataContracts.Requests;
using System;
using System.Linq;
using System.Collections.Generic;
using UnitTests.Integration.DataSets;
using Moq;
using Microsoft.AspNetCore.Identity;
using SurveyPortal.Data.Entities;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Integration.Repositories
{
    public class SurveySubmissionTests: Base
    {
        [SetUp]
        public void SetUp()
        {
            UsingDatabase(context =>
            {
                context.Answers.AddRange(AnswersDataSet.Data());
                context.SelectedAnswers.AddRange(SelectedAnswerDataSet.Data());
                context.Questions.AddRange(QuestionDataSet.Data());
                context.Surveys.AddRange(SurveyDataSet.Data());
                context.SurveySubmissions.AddRange(SurveySubmissionDataSet.Data());
                context.SaveChanges();
            });
        }

        [Test]
        public void ShouldGetSubmissionById()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveySubmissionRepository(context);

                var submissions = repo.GetBySurveyId(Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"));

                Assert.That(submissions.Count, Is.EqualTo(1));
                Assert.That(submissions[0].Id, Is.EqualTo(Guid.Parse("e870f45e-67d3-42be-acf1-317bf8227008")));
            });
        }

        [Test]
        public void ShoulCreateSubmission()
        {
            UsingDatabase(context =>
            {
                var repo = new SurveySubmissionRepository(context);

                var submission = repo.Create(new CreateSurveySubmissionRequest { 
                    Id = Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"),
                    questions = new List<CreateSurveySubmissionQuestion>
                    {
                        new CreateSurveySubmissionQuestion
                        {
                            type = 1,
                            id = Guid.Parse("6c662895-0898-4647-8804-7480ea0834ee"),
                            data = "[{\"Id\":\"6ab43d56-8a92-41de-856b-d1e71a47ab2d\",\"Checked\":false},{\"Id\":\"f870a965-f4ec-42f4-82fd-203a496b94d6\",\"Checked\":false},{\"Id\":\"d2966cfa-dcf0-4f18-a0bd-73de0137b8e5\",\"Checked\":true}]"
                        },
                        new CreateSurveySubmissionQuestion
                        {
                            type = 0,
                            id = Guid.Parse("6a26af10-0346-44fb-b826-40b1ef7c8109"),
                            data = "[{\"Id\":\"cd5d90b5-4964-4814-99fb-de8cb0880247\",\"Checked\":false},{\"Id\":\"132d5e56-0e4e-4196-8aef-0ca783d18338\",\"Checked\":false},{\"Id\":\"ee0e755d-c40c-4c25-91bd-326d882e87b3\",\"Checked\":true}]",
                        },
                        new CreateSurveySubmissionQuestion
                        {
                            type = 2,
                            id = Guid.Parse("dd111c7f-55ab-4499-9baf-809ac5d943f1"),
                            data = "text"
                        },
                        new CreateSurveySubmissionQuestion
                        {
                            type = 3,
                            id = Guid.Parse("0402e810-5772-451f-bc3c-2b17f3abab5e"),
                            data = "5"
                        },
                    }
                }, "userid");

                var newSubmission = context.SurveySubmissions.First(x => x.Id == submission.Id);
                
                Assert.That(newSubmission.SurveyId, Is.EqualTo(Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4")));
                Assert.That(newSubmission.UserId, Is.EqualTo("userid"));

                var answers = context.Answers.Where(x => x.SurveySubmissionId == newSubmission.Id);
                var selectedAnswers = context.SelectedAnswers.Where(x => answers.Select(y => y.Id).Contains(x.AnswerId)).ToList();

                Assert.That(selectedAnswers.Count, Is.EqualTo(6));

                var radioAnswer = answers.First(x => x.QuestionId == Guid.Parse("6c662895-0898-4647-8804-7480ea0834ee"));
                Assert.That(selectedAnswers.First(x => x.QuestionAnswerId == Guid.Parse("6ab43d56-8a92-41de-856b-d1e71a47ab2d")).Selected, Is.EqualTo("False"));
                Assert.That(selectedAnswers.First(x => x.QuestionAnswerId == Guid.Parse("f870a965-f4ec-42f4-82fd-203a496b94d6")).Selected, Is.EqualTo("False"));
                Assert.That(selectedAnswers.First(x => x.QuestionAnswerId == Guid.Parse("d2966cfa-dcf0-4f18-a0bd-73de0137b8e5")).Selected, Is.EqualTo("True"));

                var checkQuestion = answers.First(x => x.QuestionId == Guid.Parse("6a26af10-0346-44fb-b826-40b1ef7c8109"));
                Assert.That(selectedAnswers.First(x => x.QuestionAnswerId == Guid.Parse("cd5d90b5-4964-4814-99fb-de8cb0880247")).Selected, Is.EqualTo("False"));
                Assert.That(selectedAnswers.First(x => x.QuestionAnswerId == Guid.Parse("132d5e56-0e4e-4196-8aef-0ca783d18338")).Selected, Is.EqualTo("False"));
                Assert.That(selectedAnswers.First(x => x.QuestionAnswerId == Guid.Parse("ee0e755d-c40c-4c25-91bd-326d882e87b3")).Selected, Is.EqualTo("True"));

                var textAnswer = answers.First(x => x.QuestionId == Guid.Parse("dd111c7f-55ab-4499-9baf-809ac5d943f1"));
                Assert.That(textAnswer.Text, Is.EqualTo("text"));
                
                var rangeAnswer = answers.First(x => x.QuestionId == Guid.Parse("0402e810-5772-451f-bc3c-2b17f3abab5e"));
                Assert.That(rangeAnswer.Text, Is.EqualTo("5"));
            });
        }

        [Test]
        public void ShouldUpdate()
        {
            UsingDatabase(context =>
            {
                var mockStore = Mock.Of<IUserStore<User>>();
                Mock<UserManager<User>> MockUserManager = new Mock<UserManager<User>>(mockStore, null, null, null, null, null, null, null, null);
                MockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                    .ReturnsAsync(new User { 
                        UserName = "User1",    
                    });

                var repo = new SurveySubmissionRepository(context);

                var data = repo.GetSurveyQuestionResults(context.Surveys.Include(q => q.Questions).First(), MockUserManager.Object);

                var radioResult = data.First(x => x.Type == "radio");
                Assert.That(radioResult.Rows.Count, Is.EqualTo(3));
                Assert.That(radioResult.Rows.First(x => x.Label == "Radio1").Amount, Is.EqualTo(0));
                Assert.That(radioResult.Rows.First(x => x.Label == "Radio2").Amount, Is.EqualTo(0));
                Assert.That(radioResult.Rows.First(x => x.Label == "Radio3").Amount, Is.EqualTo(100));

                var checkResult = data.First(x => x.Type == "check");
                Assert.That(checkResult.Rows.Count, Is.EqualTo(3));
                Assert.That(checkResult.Rows.First(x => x.Label == "Check1").Amount, Is.EqualTo(0));
                Assert.That(checkResult.Rows.First(x => x.Label == "Check2").Amount, Is.EqualTo(0));
                Assert.That(checkResult.Rows.First(x => x.Label == "Check3").Amount, Is.EqualTo(100));

                var textResult = data.First(x => x.Type == "text");
                Assert.That(textResult.Rows.Count, Is.EqualTo(1));
                Assert.That(textResult.Rows.First().TextArea, Is.EqualTo("Best color"));
                Assert.That(textResult.Rows.First().UserName, Is.EqualTo("User1"));
                
                var rangeResult = data.First(x => x.Type == "range");
                Assert.That(rangeResult.Rows.Count, Is.EqualTo(1));
                Assert.That(rangeResult.Rows.First().Amount, Is.EqualTo(50));
            });
        }
    }
}
