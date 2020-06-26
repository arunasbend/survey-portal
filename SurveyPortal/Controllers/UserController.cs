using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SurveyPortal.Data.Entities;
using SurveyPortal.DataContracts.Requests;
using SurveyPortal.DataContracts.Responses;
using System;
using System.Linq;
using System.Threading.Tasks;
using SurveyPortal.Services;

namespace SurveyPortal.Controllers
{
    [Authorize]
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public UserController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<IdentityCookieOptions> identityCookieOptions,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return BadRequest();
            }

            user.Email = request.Email;
            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;
            user.UserName = request.Username;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [Route("get-user")]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return Ok(new GetCurrentUserResponse
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Username = user.UserName,
                Id = user.Id
            });
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LoginResponse
                {
                    ErrorMessage = "Fill in required fields"
                });
            }

            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, true, false);

            if (!result.Succeeded)
            {
                return BadRequest(new LoginResponse
                {
                    ErrorMessage = "Password was incorrect"
                });
            }

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return BadRequest(new LoginResponse
                {
                    ErrorMessage = "Username doesn't exist"
                });
            }

            return Ok(new LoginResponse
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Username = user.UserName,
                Id = user.Id,
            });
        }

        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                UserGroupId = null
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    ErrorMessage = string.Join(Environment.NewLine, result.Errors.Select(x => x.Description))
                });
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            return CreatedAtAction(nameof(Register), new LoginResponse
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Username = user.UserName,
                Id = user.Id,
            });
        }

        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest();
            }

            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailService.SendAsync(user.Email, "Reset Password", $"Please reset your password by using this http://sa-delta.azurewebsites.net/{code}");
            
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}