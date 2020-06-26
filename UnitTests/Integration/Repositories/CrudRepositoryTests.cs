using System;
using System.Linq;
using NUnit.Framework;
using SurveyPortal.Data;
using SurveyPortal.Data.Entities;
using UnitTests.Integration.DataSets;

namespace UnitTests.Integration.Repositories
{
    [TestFixture]
    public class CrudRepositoryTests: Base
    {
        [SetUp]
        public void Setup()
        {
            UsingDatabase(context =>
            {
                context.UserGroups.AddRange(UserGroupDataSet.Data());
                context.SaveChanges();
            });
        }

        [Test]
        public void ShouldGetAll()
        {
            UsingDatabase(context =>
            {
                var repo = new UserGroupRepository(context);

                var data = repo.GetAll();

                Assert.That(data.Count, Is.EqualTo(2));
                Assert.That(data.First(x => x.Title == "Admins").Id, Is.EqualTo(Guid.Parse("98d5f60b-f88f-4a56-9827-559b71d1c83c")));
                Assert.That(data.First(x => x.Title == "Users").Id, Is.EqualTo(Guid.Parse("829acaac-6a9f-432a-8e1f-9f625aff1a05")));
            });
        }

        [Test]
        public void ShouldGetById()
        {
            UsingDatabase(context =>
            {
                var repo = new UserGroupRepository(context);

                var data = repo.GetById(Guid.Parse("98d5f60b-f88f-4a56-9827-559b71d1c83c"));

                Assert.That(data.Title, Is.EqualTo("Admins"));
            });
        }

        [Test]
        public void ShouldBeAbleToDeleteUserGroup()
        {
            UsingDatabase(context =>
            {
                var repo = new UserGroupRepository(context);

                repo.Delete(Guid.Parse("98d5f60b-f88f-4a56-9827-559b71d1c83c"));

                var data = repo.GetAll();

                Assert.That(data.Count, Is.EqualTo(1));
                Assert.That(data[0].Id, Is.EqualTo(Guid.Parse("829acaac-6a9f-432a-8e1f-9f625aff1a05")));
                Assert.That(data[0].Title, Is.EqualTo("Users"));
            });
        }

        [Test]
        public void ShouldBeAbleToHandleErrorsOnDelete()
        {
            UsingDatabase(context =>
            {
                var repo = new GenericCrudRepository<UserGroup, object, object>(null, context.UserGroups);

                var result = repo.Delete(Guid.Parse("98d5f60b-f88f-4a56-9827-559b71d1c83c"));
                Assert.That(result, Is.EqualTo(false));

                var data = repo.GetAll();

                Assert.That(data.Count, Is.EqualTo(2));
            });
        }
    }
}
