using System;
using NUnit.Framework;
using SurveyPortal.Data;
using SurveyPortal.Data.Entities;
using UnitTests.Integration.DataSets;

namespace UnitTests.Integration.Repositories
{
    [TestFixture]
    public class EntityTests : Base
    {
        [Test]
        public void ShouldCoverAllUserGroupEntity()
        {
            UsingDatabase(context =>
            {
                context.UserGroups.AddRange(UserGroupDataSet.Data());
                context.SaveChanges();

                var repo = new UserGroupRepository(context);

                var data = repo.GetById(Guid.Parse("98d5f60b-f88f-4a56-9827-559b71d1c83c"));

                Assert.That(data.Title, Is.EqualTo("Admins"));
            });
        }

        [Test]
        public void ShouldCoverAllSurveyEntity()
        {
            UsingDatabase(context =>
            {
                context.Surveys.AddRange(SurveyDataSet.Data());
                context.SaveChanges();

                var repo = new SurveyRepository(context);

                var data = repo.GetById(Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4"));

                Assert.That(data.Title, Is.EqualTo("Survey1"));
                Assert.That(data.Description, Is.EqualTo("Description"));
                Assert.That(data.Status, Is.EqualTo(Status.Published));
            });
        }

        [Test]
        public void ShouldCoverAllSurveySubmissionEntity()
        {
            UsingDatabase(context =>
            {
                context.SurveySubmissions.AddRange(SurveySubmissionDataSet.Data());
                context.SaveChanges();

                var repo = new SurveySubmissionRepository(context);

                var data = repo.GetById(Guid.Parse("e870f45e-67d3-42be-acf1-317bf8227008"));

                Assert.That(data.Id, Is.EqualTo(Guid.Parse("e870f45e-67d3-42be-acf1-317bf8227008")));
                Assert.That(data.UserId, Is.EqualTo("10f9cf2f-d0dc-4396-817e-1293a92eacf3"));
                Assert.That(data.SurveyId, Is.EqualTo(Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4")));
            });
        }

        [Test]
        public void ShouldCoverAllQuestionEntity()
        {
            UsingDatabase(context =>
            {
                context.Questions.AddRange(QuestionDataSet.Data());
                context.SaveChanges();

                var repo = new QuestionRepository(context);

                var data = repo.GetById(Guid.Parse("6c662895-0898-4647-8804-7480ea0834ee"));

                Assert.That(data.Id, Is.EqualTo(Guid.Parse("6c662895-0898-4647-8804-7480ea0834ee")));
                Assert.That(data.Title, Is.EqualTo("What is the best color"));
                Assert.That(data.Description, Is.EqualTo("Describe the best color"));
                Assert.That(data.Mandatory, Is.EqualTo(true));
                Assert.That(data.Type, Is.EqualTo(QuestionType.radio));
                Assert.That(data.SurveyId, Is.EqualTo(Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4")));
            });
        }
    }
}
