using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PCStore.Application.Features.CQRSDesignPattern.Commands.UserCommands;
using PCStore.Application.Features.CQRSDesignPattern.Queries.UserQueries;
using PCStore.Application.Services.UserService;
using PCStore.Application.Services.UserService.ServiceDTO;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PCStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : BaseController
    {

        private readonly IUserService _userService = userService;

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminTest()
        {
            var result = User.Identity?.Name;
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserLoginQuery user, CancellationToken cancellation = default)
        {
            var result = await _userService.UserLogin(user, cancellation);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost("Account/Create")]
        public async Task<IActionResult> AccountCreate([FromBody]CreateUserCommand request, CancellationToken cancellation = default)
        {
            var confirmationLink = Url.Action("EmailConfirm", "User",
                new { userId = "{userId}", token = "{token}"},
                protocol: Request.Scheme);
            if (confirmationLink is null)
                return BadRequest("Something went wrong...");
            var req = new CreateUser { User = request };
            req.Url = confirmationLink;
            var result = await _userService.CreateUser(req, cancellation);
            return StatusCode(result.StatusCode, result.Data);
        }

        [HttpPost("Account/Update")]
        public async Task<IActionResult> AccountUpdate([FromBody] UpdateUserCommand request, CancellationToken cancellation = default)
        {
            var result = await _userService.UpdateUser(request,cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("Account/ResetPassword")]
        public async Task<IActionResult> AccountResetPassword([FromQuery]string userId, [FromQuery]string token, [FromQuery]string password, CancellationToken cancellation = default) 
        {
            token = WebUtility.UrlDecode(token);
            token = token.Replace(' ', '+');
            var request = new UpdatePasswordCommand
            {
                UserId = userId,
                Token = token,
                Password = password
            };
            var result = await _userService.ResetPassword(request,cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("Email/ResetPassword")]
        public async Task<IActionResult> EmailResetPassword([FromBody] PasswordResetEmail request, CancellationToken cancellation = default)
        {
            request.Url = "https://localhost:7256/resetpassword?userId={userId}&token={token}";
            var result = await _userService.SendPasswordResetTokenToMail(request, cancellation);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("Email/Confirm")]
        public async Task<IActionResult> EmailConfirm([FromQuery]string userId, [FromQuery]string token, CancellationToken cancellationToken = default) 
        {
            var request = new ConfirmEmail { UserId = userId, Token = token };
            var result = await _userService.ConfirmEmail(request, cancellationToken);
            return StatusCode(result.StatusCode,result);
        }
    }
}
