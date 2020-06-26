using System;
using System.Linq;
using NUnit.Framework;
using SurveyPortal.Data;
using SurveyPortal.DataContracts.Requests;
using UnitTests.Integration.DataSets;

namespace UnitTests.Integration.Repositories
{
    [TestFixture]
    public class AnswerRepositoryTests: Base
    {
        [SetUp]
        public void Setup()
        {
            UsingDatabase(context =>
            {
                context.Answers.AddRange(AnswersDataSet.Data());
                context.SaveChanges();
            });
        }

        [Test]
        public void ShouldCreateNewAnswer()
        {
            UsingDatabase(context =>
            {
                var repo = new AnswerRepository(context);

                var entity = new SurveyPortal.Data.Entities.Answer
                {
                    Text = "New Answer",
                };

                var before = repo.GetAll();

                repo.Create(entity);

                var after = repo.GetAll();

                Assert.That(after.Count, Is.EqualTo(before.Count + 1));
                Assert.That(after.FirstOrDefault(x => x.Text == "New Answer"), Is.Not.Null);
            });
        }

        [Test]
        public void ShouldBeAbleToUpdateAnswerTitle()
        {
            UsingDatabase(context =>
            {
                var repo = new AnswerRepository(context);

                repo.Update(Guid.Parse("2377a487-5a04-4452-b883-4085c6ea5cbd"), new UpdateAnswerRequest
                {
                    Text = "Updated",
                });

                var data = repo.GetAll();

                Assert.That(data.Count, Is.EqualTo(8));
                Assert.That(data.First(x => x.Id == Guid.Parse("2377a487-5a04-4452-b883-4085c6ea5cbd")).Text, Is.EqualTo("Updated"));
            });
        }
    }
}
