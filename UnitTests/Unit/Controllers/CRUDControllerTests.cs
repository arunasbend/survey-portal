using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SurveyPortal.Controllers;
using SurveyPortal.Data;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Requests;

namespace UnitTests.Unit.Controllers
{
    [TestFixture]
    public class CrudControllerTests: ControllerTestBase
    {
        [Test]
        public void ShouldReturnById_Success()
        {
            var mock = new Mock<IAnswerRepository>();
            mock.Setup(x => x.GetById(It.IsAny<Guid>()))
              .Returns(new Answer
              {
                  Text = "Answer",
              });

            var controller = new AnswerController(mock.Object);

            var actionResult = controller.GetById(new Guid());

            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
            Assert.That(((actionResult as OkObjectResult)?.Value as dynamic)?.Text, Is.EqualTo("Answer"));
        }

        [Test]
        public void ShouldReturn404IfItemIsNotFound()
        {
            var mock = new Mock<IAnswerRepository>();
            mock.Setup(x => x.GetById(It.IsAny<Guid>()))
                .Throws<InvalidOperationException>();

            var controller = new AnswerController(mock.Object);

            var actionResult = controller.GetById(new Guid());

            Assert.That(actionResult, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void ShouldBeAbleToValidateCreateRequestModel()
        {
            var mock = new Mock<IUserGroupRepository>();

            var controller = new UserGroupController(mock.Object);

            var request = new CreateUserGroupRequest();

            ObjectValidatorExecutor(controller, request);

            var actionResult = controller.Create(request);

            Assert.That(actionResult, Is.InstanceOf<BadRequestObjectResult>());

            Dictionary<string, object> a = ((actionResult as BadRequestObjectResult)?.Value as dynamic);
            Assert.That(a.FirstOrDefault(x => x.Key == "Title").Key, Is.EqualTo("Title"));
            Assert.That((a.FirstOrDefault(x => x.Key == "Title").Value as dynamic)[0], Is.EqualTo("The Title field is required."));
        }

        [Test]
        public void ShouldBeAbleToValidateUpdateRequestModel()
        {
            var mock = new Mock<IUserGroupRepository>();
            
            var controller = new UserGroupController(mock.Object);

            var request = new UpdateUserGroupRequest();

            ObjectValidatorExecutor(controller, request);

            var actionResult = controller.Update(Guid.Empty, request);

            Assert.That(actionResult, Is.InstanceOf<BadRequestObjectResult>());

            Dictionary<string, object> a = ((actionResult as BadRequestObjectResult)?.Value as dynamic);
            Assert.That(a.FirstOrDefault(x => x.Key == "Title").Key, Is.EqualTo("Title"));
            Assert.That((a.FirstOrDefault(x => x.Key == "Title").Value as dynamic)[0], Is.EqualTo("The Title field is required."));
        }

        [Test]
        public void ShouldThrowBadRequestIfUpdateFails()
        {
            var mock = new Mock<IUserGroupRepository>();
            
            mock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<UpdateUserGroupRequest>()))
                .Throws<InvalidOperationException>();

            var controller = new UserGroupController(mock.Object);

            var request = new UpdateUserGroupRequest();

            var actionResult = controller.Update(Guid.Empty, request);

            Assert.That(actionResult, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public void ShouldReturnNoContent_OnDelete()
        {
            var mock = new Mock<IAnswerRepository>();
            
            var controller = new AnswerController(mock.Object);

            var actionResult = controller.Delete(new Guid());

            Assert.That(actionResult, Is.InstanceOf<NoContentResult>());
        }

        [TestCase("InvalidOperationException")]
        [TestCase("Exception")]
        public void ShouldReturnException_OnDelete(string e)
        {
            var mock = new Mock<IAnswerRepository>();
            var controller = new AnswerController(mock.Object);

            if (e == "InvalidOperationException")
            {
                mock.Setup(foo => foo.Delete(It.IsAny<Guid>())).Throws<InvalidOperationException>();
            }
            else {
                mock.Setup(foo => foo.Delete(It.IsAny<Guid>())).Throws<Exception>();
            }

            var actionResult = controller.Delete(new Guid());

            if (e == "InvalidOperationException")
            {
                Assert.That(actionResult, Is.InstanceOf<NotFoundResult>());
            }
            else {
                Assert.That(actionResult, Is.InstanceOf<BadRequestResult>());            
            }
        }
    }
}