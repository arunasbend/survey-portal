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
    public class QuestionControllerTests
    {
        [Test]
        public void Question_ShouldHaveCorrectEntityType()
        {
            var mock = new Mock<IQuestionRepository>();
            mock.Setup(x => x.GetAll()).Returns(new List<Question>
            {
                new Question
                {
                    Title = "Question",
                },
            });

            var controller = new QuestionController(mock.Object);

            var items = controller.GetAll();

            Assert.That(items[0].Title, Is.EqualTo("Question"));
            Assert.That(items[0], Is.InstanceOf(typeof(Question)));
        }

        [Test]
        public void Question_ShouldHaveCorrectUpdateTypes() {
            var mock = new Mock<IQuestionRepository>();

            mock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<UpdateQuestionRequest>()))
                .Callback((Guid id, UpdateQuestionRequest request) =>
                {
                    Assert.That(request.Title, Is.EqualTo("Text"));
                })
                .Verifiable();

            var controller = new QuestionController(mock.Object);

            controller.Update(Guid.NewGuid(), new UpdateQuestionRequest
            {
                Title = "Text",
            });

            mock.Verify();
        }

        [Test]
        public void Question_ShouldHaveCorrectCreateType()
        {
            var mock = new Mock<IQuestionRepository>();

            mock.Setup(x => x.Create(It.IsAny<CreateQuestionRequest>()))
                .Returns((CreateQuestionRequest request) =>
                {
                    Assert.That(request.Title, Is.EqualTo("Create"));

                    return new Question
                    {
                        Id = Guid.Empty,
                    };
                });

            var controller = new QuestionController(mock.Object);

            controller.Create(new CreateQuestionRequest
            {
                Title = "Create",
            });
        }
    }
}