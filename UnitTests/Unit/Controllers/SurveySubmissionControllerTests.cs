using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SurveyPortal.Controllers;
using SurveyPortal.Data;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Requests;
using SurveyPortal.DataContracts.Responses;
using UnitTests.Integration.DataSets;

namespace UnitTests.Unit.Controllers
{
    [TestFixture]
    public class SurveySubmissionControllerTests : ControllerTestBase
    {
        private Mock<ISurveySubmissionRepository> MockSurveySubmissionRepo { get; set; }
        private Mock<ISurveyRepository> MockSurveyRepo { get; set; }
        private Mock<UserManager<User>> MockUserManager { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockStore = Mock.Of<IUserStore<User>>();
            MockUserManager = new Mock<UserManager<User>>(mockStore, null, null, null, null, null, null, null, null);
            MockSurveyRepo = new Mock<ISurveyRepository>();
            MockSurveySubmissionRepo = new Mock<ISurveySubmissionRepository>();
        }
        
        [Test]
        public void ItShouldValidateIfSurveyWithTheKeyExists()
        {
            MockSurveyRepo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(null as Survey);

            var controller = new SurveySubmissionController(MockSurveySubmissionRepo.Object, MockSurveyRepo.Object, MockUserManager.Object);

            var response = controller.GetSubmissionResults(Guid.Empty);

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());

            ErrorResponse error = (response as BadRequestObjectResult)?.Value as dynamic;

            Assert.That(error.ErrorMessage, Is.EqualTo("There are no surveys with that Id."));
        }
        
        [Test]
        public void ItShouldNotAllowEmptySubmissionListToBeReturnedSuccessfully()
        {
            MockSurveyRepo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(SurveyDataSet.Data()[0]);
            MockSurveySubmissionRepo.Setup(x => x.GetBySurveyId(It.IsAny<Guid>())).Returns(new List<SurveySubmission>());

            var controller = new SurveySubmissionController(MockSurveySubmissionRepo.Object, MockSurveyRepo.Object, MockUserManager.Object);

            var response = controller.GetSubmissionResults(Guid.Empty);

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());

            ErrorResponse error = (response as BadRequestObjectResult)?.Value as dynamic;

            Assert.That(error.ErrorMessage, Is.EqualTo("There are no survey submissions for the given survey."));
        }

        [Test]
        public void ItShouldGetAllOtherErrorsAndReturnAsSimpleResponse()
        {
            MockSurveyRepo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(SurveyDataSet.Data()[0]);
            MockSurveySubmissionRepo.Setup(x => x.GetBySurveyId(It.IsAny<Guid>())).Returns(SurveySubmissionDataSet.Data());
            MockSurveySubmissionRepo.Setup(x => x.GetSurveyQuestionResults(It.IsAny<Survey>(), It.IsAny<UserManager<User>>())).Throws<InvalidOperationException>();

            var controller = new SurveySubmissionController(MockSurveySubmissionRepo.Object, MockSurveyRepo.Object, MockUserManager.Object);

            var response = controller.GetSubmissionResults(Guid.Empty);

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void ItShouldGetAggregatedSubmissionsBySurveyId()
        {
            MockSurveyRepo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(SurveyDataSet.Data()[0]);
            MockSurveySubmissionRepo.Setup(x => x.GetBySurveyId(It.IsAny<Guid>())).Returns(SurveySubmissionDataSet.Data());
            MockSurveySubmissionRepo.Setup(x => x.GetSurveyQuestionResults(It.IsAny<Survey>(), It.IsAny<UserManager<User>>())).Returns(new List<SurveyResultsQuestion>
            {
                new SurveyResultsQuestion
                {
                    Id = Guid.Parse("8f64969f-2a6b-45f0-aeff-d3f98ea4d924"),
                    Title = "Question1",
                    Type = "radio",
                    Rows = new List<SurveyResultsRow>
                    {
                        new SurveyResultsRow
                        {
                            Id = Guid.Parse("3a4f9f4b-d816-42e0-ad01-14cbbae8a32d"),
                            UserName = "UserName",
                            Label = "Label",
                            TextArea = "TextArea",
                            Amount = 5,
                        }
                    }
                }
            });

            var controller = new SurveySubmissionController(MockSurveySubmissionRepo.Object, MockSurveyRepo.Object, MockUserManager.Object);

            var response = controller.GetSubmissionResults(Guid.Empty);

            Assert.That(response, Is.InstanceOf<OkObjectResult>());

            SurveyResultsResponse result = (response as OkObjectResult)?.Value as dynamic;

            Assert.That(result.Heading.Title, Is.EqualTo("Survey1"));
            Assert.That(result.Heading.Description, Is.EqualTo("Description"));
            Assert.That(result.Heading.TotalSubmissions, Is.EqualTo(1));
            Assert.That(result.Heading.SurveyId, Is.EqualTo(Guid.Parse("83e88952-5911-4c64-81c3-2770658e32f4")));
            Assert.That(result.Heading.DueDate, Is.EqualTo("2020-02-01"));
         
            Assert.That(result.Questions.Count, Is.EqualTo(1));
            Assert.That(result.Questions[0].Id, Is.EqualTo(Guid.Parse("8f64969f-2a6b-45f0-aeff-d3f98ea4d924")));
            Assert.That(result.Questions[0].Title, Is.EqualTo("Question1"));
            Assert.That(result.Questions[0].Type, Is.EqualTo("radio"));
            Assert.That(result.Questions[0].Rows.Count, Is.EqualTo(1));
            Assert.That(result.Questions[0].Rows[0].Id, Is.EqualTo(Guid.Parse("3a4f9f4b-d816-42e0-ad01-14cbbae8a32d")));
            Assert.That(result.Questions[0].Rows[0].UserName, Is.EqualTo("UserName"));
            Assert.That(result.Questions[0].Rows[0].Label, Is.EqualTo("Label"));
            Assert.That(result.Questions[0].Rows[0].TextArea, Is.EqualTo("TextArea"));
            Assert.That(result.Questions[0].Rows[0].Amount, Is.EqualTo(5));
        }

        [Test]
        public void ItShouldValidateCreateSubmissionRequest()
        {
            var controller = new SurveySubmissionController(MockSurveySubmissionRepo.Object, MockSurveyRepo.Object, MockUserManager.Object);

            var request = new CreateSurveySubmissionRequest();

            ObjectValidatorExecutor(controller, request);

            var response = controller.Create(request);

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void ItShouldCreateSurveySubmissionUsingUserContext()
        {
            var request = new CreateSurveySubmissionRequest
            {
                Id = Guid.Empty,
                questions = new List<CreateSurveySubmissionQuestion>
                {
                    new CreateSurveySubmissionQuestion
                    {
                        id = Guid.Empty,
                        title = "title",
                        type = 2,
                        data = null,
                    }
                }
            };

            MockSurveySubmissionRepo.Setup(x => x.Create(It.IsAny<CreateSurveySubmissionRequest>(), It.IsAny<string>())).Verifiable();

            var controller = new SurveySubmissionController(MockSurveySubmissionRepo.Object, MockSurveyRepo.Object, MockUserManager.Object);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock
                .Setup(x => x.User)
                .Returns(new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "UserId") }, "AuthType")));
                
            controller.ControllerContext.HttpContext = httpContextMock.Object;
            
            var response = controller.Create(request);

            Assert.That(response, Is.InstanceOf<NoContentResult>());

            MockSurveySubmissionRepo.Verify(x => x.Create(request, "UserId"));
        }

        [Test]
        public void SurveySubmission_Create_ItShouldReturnAllErrorsAs400()
        {
            var request = new CreateSurveySubmissionRequest
            {
                Id = Guid.Empty,
            };

            var controller = new SurveySubmissionController(MockSurveySubmissionRepo.Object, MockSurveyRepo.Object, MockUserManager.Object);

            var response = controller.Create(request);

            Assert.That(response, Is.InstanceOf<BadRequestResult>());
        }
    }
}
