using System;
using System.Linq;
using NUnit.Framework;
using SurveyPortal.Data;
using SurveyPortal.DataContracts.Requests;
using UnitTests.Integration.DataSets;

namespace UnitTests.Integration.Repositories
{
    [TestFixture]
    public class UserGroupRepositoryTests: Base
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
        public void ShouldGetByTitle()
        {
            UsingDatabase(context =>
            {
                var repo = new UserGroupRepository(context);

                var data = repo.GetByTitle("Users");

                Assert.That(data.Title, Is.EqualTo("Users"));
                Assert.That(data.Id, Is.EqualTo(Guid.Parse("829acaac-6a9f-432a-8e1f-9f625aff1a05")));
            });
        }

        [Test]
        public void ShouldCreateNewUserGroup()
        {
            UsingDatabase(context =>
            {
                var repo = new UserGroupRepository(context);

                var before = repo.GetAll();

                repo.Create(new CreateUserGroupRequest
                {
                    Title = "New Group",
                });

                var after = repo.GetAll();

                Assert.That(after.Count, Is.EqualTo(before.Count + 1));
                Assert.That(after.FirstOrDefault(x => x.Title == "New Group"), Is.Not.Null);
            });
        }

        [Test]
        public void ShouldBeAbleToUpdateUserGroupTitle()
        {
            UsingDatabase(context =>
            {
                var repo = new UserGroupRepository(context);

                repo.Update(Guid.Parse("98d5f60b-f88f-4a56-9827-559b71d1c83c"), new UpdateUserGroupRequest
                {
                    Title = "Updated",
                });

                var data = repo.GetAll();

                Assert.That(data.Count, Is.EqualTo(2));
                Assert.That(data.First(x => x.Id == Guid.Parse("98d5f60b-f88f-4a56-9827-559b71d1c83c")).Title, Is.EqualTo("Updated"));
            });
        }
    }
}
