using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SurveyPortal.Controllers;
using SurveyPortal.Data;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Dto;
using SurveyPortal.DataContracts.Responses;

namespace UnitTests.Unit.Controllers
{
    [TestFixture]
    public class SurveyControllerTests
    {
        [Test]
        public void Survey_ShouldHaveCorrectEntityType()
        {
            var mock = new Mock<ISurveyRepository>();
            mock.Setup(x => x.GetAll()).Returns(new List<Survey>
            {
                new Survey
                {
                    Title = "Survey",
                },
            });

            var controller = new SurveyController(mock.Object);

            var items = controller.GetAll();

            Assert.That(items[0].Title, Is.EqualTo("Survey"));
            Assert.That(items[0], Is.InstanceOf(typeof(Survey)));
        }

        [Test]
        public void Survey_ShouldHaveCorrectUpdateTypes() {
            var mock = new Mock<ISurveyRepository>();

            mock.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<SurveyDto>()))
                .Callback((Guid id, SurveyDto request) =>
                {
                    Assert.That(request.Title, Is.EqualTo("Text"));
                })
                .Verifiable();

            var controller = new SurveyController(mock.Object);

            controller.Update(Guid.NewGuid(), new SurveyDto
            {
                Title = "Text",
            });

            mock.Verify();
        }

        [Test]
        public void Survey_ShouldHaveCorrectCreateType()
        {
            var mock = new Mock<ISurveyRepository>();

            mock.Setup(x => x.Create(It.IsAny<SurveyDto>()))
                .Returns((SurveyDto request) =>
                {
                    Assert.That(request.Title, Is.EqualTo("Create"));

                    return new Survey
                    {
                        Id = Guid.Empty,
                    };
                });

            var controller = new SurveyController(mock.Object);

            controller.Create(new SurveyDto
            {
                Title = "Create",
            });
        }
        
        [Test]
        public void Survey_ShouldReturnById_Success()
        {
            var mock = new Mock<ISurveyRepository>();
            mock.Setup(x => x.GetForUpdateById(It.IsAny<Guid>()))
              .Returns(new SurveyDto
              {
                  Title = "Survey",
              });

            var controller = new SurveyController(mock.Object);

            var actionResult = controller.GetForUpdateById(new Guid());

            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            SurveyDto dto = ((actionResult as OkObjectResult)?.Value as dynamic);
            
            Assert.That(dto.Title, Is.EqualTo("Survey"));
        }

        [Test]
        public void Survey_ShouldReturn404IfItemIsNotFound()
        {
            var mock = new Mock<ISurveyRepository>();
            mock.Setup(x => x.GetForUpdateById(It.IsAny<Guid>()))
                .Throws<InvalidOperationException>();

            var controller = new SurveyController(mock.Object);

            var actionResult = controller.GetForUpdateById(new Guid());

            Assert.That(actionResult, Is.InstanceOf<NotFoundResult>());
        }
        
        [Test]
        public void Survey_ShouldReturnOverview_Success()
        {
            var mock = new Mock<ISurveyRepository>();
            mock.Setup(x => x.GetSurveyOverview())
                .Returns(new SurveyOverviewResponse
                {
                    Header = new SurveyOverviewResponseHeader
                    {
                        StatisticsData = new List<StatisticsData>
                        {
                            new StatisticsData
                            {
                                Title = "Stat1",
                                Count = 50,
                            }
                        },
                        SurveyData = new List<SurveyData>
                        {
                            new SurveyData
                            {
                                Title = "Survey1",
                                Count = 100,
                            }
                        }
                    },
                    SurveyManager = new List<SurveyManager>
                    {
                        new SurveyManager
                        {
                            Name = "name",
                            Status = "status",
                        }
                    },
                });

            var controller = new SurveyController(mock.Object);

            var actionResult = controller.GetOverview();

            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());

            SurveyOverviewResponse response = ((actionResult as OkObjectResult)?.Value as dynamic);
            Assert.That(response.Header.StatisticsData[0].Title, Is.EqualTo("Stat1"));
            Assert.That(response.Header.StatisticsData[0].Count, Is.EqualTo(50));
            Assert.That(response.Header.SurveyData[0].Title, Is.EqualTo("Survey1"));
            Assert.That(response.Header.SurveyData[0].Count, Is.EqualTo(100));
            Assert.That(response.SurveyManager[0].Name, Is.EqualTo("name"));
            Assert.That(response.SurveyManager[0].Status, Is.EqualTo("status"));
        }

        [Test]
        public void Survey_ShouldReturn404IfOverviewIsNotFound()
        {
            var mock = new Mock<ISurveyRepository>();
            mock.Setup(x => x.GetSurveyOverview())
                .Throws<ArgumentNullException>();

            var controller = new SurveyController(mock.Object);

            var actionResult = controller.GetOverview();

            Assert.That(actionResult, Is.InstanceOf<NotFoundResult>());
        }
    }
}