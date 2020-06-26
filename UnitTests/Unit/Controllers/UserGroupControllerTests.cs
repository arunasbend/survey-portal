using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SurveyPortal.Controllers;
using SurveyPortal.Data;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Requests;

namespace UnitTests.Unit.Controllers
{
    [TestFixture]
    public class UserGroupControllerTests
    {
        [Test]
        public void UserGroup_ShouldHaveCorrectEntityType()
        {
            var mock = new Mock<IUserGroupRepository>();
            mock.Setup(x => x.GetAll()).Returns(new List<UserGroup>
            {
                new UserGroup
                {
                    Title = "UserGroup",
                },
            });

            var controller = new UserGroupController(mock.Object);

            var items = controller.GetAll();

            Assert.That(items[0].Title, Is.EqualTo("UserGroup"));
            Assert.That(items[0], Is.InstanceOf(typeof(UserGroup)));
        }

        [Test]
        public void UserGroup_ShouldHaveCorrectUpdateTypes() {
            var mock = new Mock<IUserGroupRepository>();

            mock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<UpdateUserGroupRequest>()))
                .Callback((Guid id, UpdateUserGroupRequest request) =>
                {
                    Assert.That(request.Title, Is.EqualTo("Text"));
                })
                .Verifiable();

            var controller = new UserGroupController(mock.Object);

            controller.Update(Guid.NewGuid(), new UpdateUserGroupRequest
            {
                Title = "Text",
            });

            mock.Verify();
        }

        [Test]
        public void UserGroup_ShouldHaveCorrectCreateType()
        {
            var mock = new Mock<IUserGroupRepository>();

            mock.Setup(x => x.Create(It.IsAny<CreateUserGroupRequest>()))
                .Returns((CreateUserGroupRequest request) =>
                {
                    Assert.That(request.Title, Is.EqualTo("Create"));

                    return new UserGroup
                    {
                        Id = Guid.Empty,
                    };
                });

            var controller = new UserGroupController(mock.Object);

            controller.Create(new CreateUserGroupRequest
            {
                Title = "Create",
            });
        }
    }
}