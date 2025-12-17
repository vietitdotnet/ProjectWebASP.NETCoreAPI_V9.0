
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using MyApp.Application.Core.Services;
using MyApp.Application.Interfaces.Identity;
using MyApp.Application.Models.Requests.Auths;
using MyApp.Application.Models.Requests.Logins;
using MyApp.Application.Models.Requests.Logout;
using MyApp.Application.Models.Requests.Registers;
using MyApp.Domain.Exceptions;
using MyApp.Infrastructure.Models;
using System.Security.Claims;

namespace MyApp.WebApi.Features.Auths
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly ILogger<AuthController> _logger;

        private readonly IEmailService _emailService;
        public AuthController(IAuthService authService, SignInManager<AppUser> signInManager , IEmailService emailService)
        {
            _authService = authService;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request.UserName, request.Password);
            return Ok(result);
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request.Token, request.RefreshToken);
            return Ok(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            await _authService.LogoutAsync(request.UserId);
            return Ok(new { Success = true, Message = "Đăng xuất thành công" });
        }

        [HttpGet("external-login")]
        public IActionResult ExternalLogin([FromQuery] string provider)
        {
            if (string.IsNullOrEmpty(provider))
                return BadRequest("Provider is required");

            var redirectUrl = Url.Action(nameof(ExternalCallback), "Auth", null, Request.Scheme);


            var properties = _signInManager
                .ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return Challenge(properties, provider);
        }


        [HttpGet("callback")]
        public async Task<IActionResult> ExternalCallback()
        {
            var result = await _authService.ExternalCallbackLoginAsync();
            return Ok(result);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
        {
            var result = await _authService
                .ConfirmEmailAsync(userId, code);

            return Ok(result);
        }
    }

}
