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
    public class AnswerControllerTests
    {
        [Test]
        public void ShouldHaveTheRightTypes()
        {
            var mock = new Mock<IAnswerRepository>();

            mock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<UpdateAnswerRequest>())).Callback((Guid id, UpdateAnswerRequest request) =>
            {
                Assert.That(request.Text, Is.EqualTo("Text"));
            });
            mock.Setup(x => x.GetAll()).Returns(new List<Answer>
            {
                new Answer
                {
                    Text = "Answer",
                },
            });

            var controller = new AnswerController(mock.Object);

            controller.Update(Guid.NewGuid(), new UpdateAnswerRequest
            {
                Text = "Text",
            });

            var items = controller.GetAll();

            Assert.That(items[0].Text, Is.EqualTo("Answer"));
            Assert.That(items[0], Is.InstanceOf(typeof(Answer)));
        }
    }
}