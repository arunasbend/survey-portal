using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SurveyPortal.Controllers;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Requests;
using SurveyPortal.DataContracts.Responses;
using SurveyPortal.Services;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace UnitTests.Unit.Controllers
{
    [TestFixture]
    public class UserControllerTests: ControllerTestBase
    {
        private Mock<IEmailService> MockEmailService { get; set; }
        private IOptions<IdentityCookieOptions> MockCookiePolicy { get; set; }
        private Mock<SignInManager<User>> MockSigninManager { get; set; }
        private Mock<UserManager<User>> MockUserManager { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockStore = Mock.Of<IUserStore<User>>();
            var mockHttpContext = new Mock<IHttpContextAccessor>();
            var mockUserClaimsFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            MockUserManager = new Mock<UserManager<User>>(mockStore, null, null, null, null, null, null, null, null);
            MockSigninManager = new Mock<SignInManager<User>>(MockUserManager.Object, mockHttpContext.Object, mockUserClaimsFactory.Object, null, null);
            MockCookiePolicy = new OptionsManager<IdentityCookieOptions>(
                new IConfigureOptions<IdentityCookieOptions>[]
                {new ConfigureOptions<IdentityCookieOptions>(options =>
                {
                })});
            MockEmailService = new Mock<IEmailService>();
        }

        [Test]
        public async Task ItShouldAllowUserToRegister()
        {
            MockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Callback((User user, string a) =>
                {
                    user.Id = "Id";
                })
                .ReturnsAsync(IdentityResult.Success);
            
            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetRegistrationDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.Register(request, "return");

            Assert.That(response, Is.InstanceOf<CreatedAtActionResult>());

            LoginResponse loginResponse = (response as CreatedAtActionResult)?.Value as dynamic;

            Assert.That(loginResponse.Id, Is.EqualTo("Id"));
            Assert.That(loginResponse.Email, Is.EqualTo("email@email.com"));
            Assert.That(loginResponse.ErrorMessage, Is.Null);
            Assert.That(loginResponse.Firstname, Is.EqualTo("Firstname"));
            Assert.That(loginResponse.Lastname, Is.EqualTo("Lastname"));
            Assert.That(loginResponse.Username, Is.EqualTo("Username"));
        }

        [Test]
        public async Task ItShouldValidateRegistrationDto()
        {
            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = new CreateUserRequest();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.Register(request, "return");

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task ItShouldNotifyIfUserCouldNotBeCreated()
        {
            MockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Callback((User user, string a) =>
                {
                    user.Id = "Id";
                })
                .ReturnsAsync(IdentityResult.Failed(new IdentityError
                {
                    Code = "20030",
                    Description = "User already exists",
                }));

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetRegistrationDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.Register(request, "return");

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());

            dynamic error = ((dynamic)response).Value;
            Assert.That(error.GetType().GetProperty("ErrorMessage").GetValue(error, null), Is.EqualTo("User already exists"));
        }

        [Test]
        public async Task ItShouldLogoutUsingIdentityServer()
        {
            MockSigninManager.Setup(x => x.SignOutAsync()).Verifiable();

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var response = await controller.Logout();

            Assert.That(response, Is.InstanceOf<NoContentResult>());

            MockSigninManager.Verify(x => x.SignOutAsync());
        }

        [Test]
        public async Task ItShouldGetCurrentUserFromIdentityServer()
        {
            MockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(GetUserEntity()).Verifiable();

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock
                .Setup(x => x.User)
                .Returns(new GenericPrincipal(new GenericIdentity("UserName"), new []{ "" }));
            controller.ControllerContext.HttpContext = httpContextMock.Object;

            var response = await controller.GetCurrentUser();
            Assert.That(response, Is.InstanceOf<OkObjectResult>());

            GetCurrentUserResponse currentUser = ((dynamic) response).Value;

            Assert.That(currentUser.Id, Is.EqualTo("Id"));
            Assert.That(currentUser.Email, Is.EqualTo("email@email.com"));
            Assert.That(currentUser.Firstname, Is.EqualTo("Firstname"));
            Assert.That(currentUser.Lastname, Is.EqualTo("Lastname"));
            Assert.That(currentUser.Username, Is.EqualTo("Username"));

            MockUserManager.Verify(x => x.FindByNameAsync("UserName"));
        }

        [Test]
        public async Task ItShouldAllowUserToLogin()
        {
            MockSigninManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<bool>(),It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);
            MockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserEntity());

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetLoginDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.Login(request);

            Assert.That(response, Is.InstanceOf<OkObjectResult>());

            LoginResponse loginResponse = (response as OkObjectResult)?.Value as dynamic;

            Assert.That(loginResponse.Id, Is.EqualTo("Id"));
            Assert.That(loginResponse.Email, Is.EqualTo("email@email.com"));
            Assert.That(loginResponse.ErrorMessage, Is.Null);
            Assert.That(loginResponse.Firstname, Is.EqualTo("Firstname"));
            Assert.That(loginResponse.Lastname, Is.EqualTo("Lastname"));
            Assert.That(loginResponse.Username, Is.EqualTo("Username"));
        }

        [Test]
        public async Task ItShouldValidateLoginDto()
        {
            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = new LoginUserRequest();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.Login(request);

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());

            LoginResponse loginResponse = (response as BadRequestObjectResult)?.Value as dynamic;
            Assert.That(loginResponse.ErrorMessage, Is.EqualTo("Fill in required fields"));
        }

        [Test]
        public async Task ItShouldValidateCredentialsOnLogin()
        {
            MockSigninManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<bool>(),It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetLoginDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.Login(request);

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());

            LoginResponse loginResponse = (response as BadRequestObjectResult)?.Value as dynamic;
            Assert.That(loginResponse.ErrorMessage, Is.EqualTo("Password was incorrect"));
        }

        [Test]
        public async Task ItShouldSendErrorCodeIfUserWasNotFoundWhenTryingToLogin()
        {
            MockSigninManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<bool>(),It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);
            MockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(null as User);

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetLoginDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.Login(request);

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());

            LoginResponse loginResponse = (response as BadRequestObjectResult)?.Value as dynamic;
            Assert.That(loginResponse.ErrorMessage, Is.EqualTo("Username doesn't exist"));
        }

        [Test]
        public async Task ItShouldValidateUserUpdateDto()
        {
            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = new UpdateUserRequest();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.UpdateAsync(Guid.Empty, request);

            Assert.That(response, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task ItShouldValidateUserBeforeUpdate()
        {
            MockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(null as User);

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetUserUpdateDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.UpdateAsync(Guid.Empty, request);

            Assert.That(response, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task ItShouldUpdateUserWithTheRightData()
        {
            MockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserEntity());
            MockUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .Callback((User user) =>
                {
                    Assert.That(user.Id, Is.EqualTo("Id"));
                    Assert.That(user.Email, Is.EqualTo("email@email.com1"));
                    Assert.That(user.Firstname, Is.EqualTo("Firstname1"));
                    Assert.That(user.Lastname, Is.EqualTo("Lastname1"));
                    Assert.That(user.UserName, Is.EqualTo("Username1"));
                })
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetUserUpdateDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.UpdateAsync(Guid.Empty, request);

            Assert.That(response, Is.InstanceOf<NoContentResult>());

            MockUserManager.Verify(x => x.UpdateAsync(It.IsAny<User>()));
        }

        [Test]
        public async Task ShouldReturn400WhenUpdateCannotBePerformed()
        {
            MockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserEntity());
            MockUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Failed());

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetUserUpdateDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.UpdateAsync(Guid.Empty, request);

            Assert.That(response, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task ItShouldValidateForgotPasswordDto()
        {
            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = new ForgotPasswordRequest();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.ForgotPassword(request);

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public async Task ItShouldValidateUserBeforeRestoringPassword()
        {
            MockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(null as User);

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetForgotPasswordDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.ForgotPassword(request);

            Assert.That(response, Is.InstanceOf<BadRequestResult>());
        }
        
        [Test]
        public async Task ItShouldSendEmailToResetPassword()
        {
            MockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserEntity());
            MockUserManager.Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<User>()))
                .ReturnsAsync("token");
            MockEmailService.Setup(x => x.SendAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetForgotPasswordDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.ForgotPassword(request);

            Assert.That(response, Is.InstanceOf<OkResult>());

            MockEmailService.Verify(x => x.SendAsync("email@email.com", "Reset Password", "Please reset your password by using this http://sa-delta.azurewebsites.net/token"));
        }

        [Test]
        public async Task ItShouldValidateResetPasswordDto()
        {
            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = new ResetPasswordRequest();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.ResetPassword(request);

            Assert.That(response, Is.InstanceOf<BadRequestObjectResult>());
        }
        
        [Test]
        public async Task ItShouldValidateUserWhenResettingPassword()
        {
            MockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(null as User);

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetResetPasswordDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.ResetPassword(request);

            Assert.That(response, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task ItShouldReturn400IfPasswordCouldNotBeReset()
        {
            MockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserEntity());
            MockUserManager.Setup(x => x.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetResetPasswordDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.ResetPassword(request);

            Assert.That(response, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task ItShouldResetPasswordSuccessfully()
        {
            MockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserEntity());
            MockUserManager.Setup(x => x.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var request = GetResetPasswordDto();

            ObjectValidatorExecutor(controller, request);

            var response = await controller.ResetPassword(request);

            Assert.That(response, Is.InstanceOf<OkResult>());
        }

        [Test]
        public async Task ItShouldDeleteUser()
        {
            MockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserEntity());
            MockUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Success);

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var response = await controller.Delete(Guid.Empty);

            Assert.That(response, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task ItShouldReturn400UponUserDeleteFailure()
        {
            MockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(GetUserEntity());
            MockUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>()))
                .ReturnsAsync(IdentityResult.Failed());

            var controller = new UserController(MockUserManager.Object, MockSigninManager.Object, MockCookiePolicy, MockEmailService.Object);

            var response = await controller.Delete(Guid.Empty);

            Assert.That(response, Is.InstanceOf<BadRequestResult>());
        }

        private CreateUserRequest GetRegistrationDto()
        {
            return new CreateUserRequest
            {
                Email = "email@email.com",
                Firstname = "Firstname",
                Lastname = "Lastname",
                Username = "Username",
                Password = "Password",
            };
        }

        private ResetPasswordRequest GetResetPasswordDto()
        {
            return new ResetPasswordRequest
            {
                Email = "email@email.com",
                Password = "Password",
                Token = "token",
            };
        }
        
        private LoginUserRequest GetLoginDto()
        {
            return new LoginUserRequest
            {
                Username = "Username",
                Password = "Password",
            };
        }

        private User GetUserEntity()
        {
            return new User
            {
                Id = "Id",
                Email = "email@email.com",
                Firstname = "Firstname",
                Lastname = "Lastname",
                UserName = "Username",
            };
        }

        private UpdateUserRequest GetUserUpdateDto()
        {
            return new UpdateUserRequest
            {
                Email = "email@email.com1",
                Firstname = "Firstname1",
                Lastname = "Lastname1",
                Username = "Username1",
                Password = "Password1",
            };
        }
        
        private ForgotPasswordRequest GetForgotPasswordDto()
        {
            return new ForgotPasswordRequest
            {
                Email = "email@email.com",
            };
        }
    }
}
