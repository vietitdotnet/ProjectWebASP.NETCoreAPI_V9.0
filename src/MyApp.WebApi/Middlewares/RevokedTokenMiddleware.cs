using Microsoft.AspNetCore.Identity;
using MyApp.Infrastructure.Constants;
using MyApp.Infrastructure.Extentions;
using MyApp.Infrastructure.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyApp.WebApi.Middlewares
{
    public class RevokedTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public RevokedTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, UserManager<AppUser> userManager)
        {


            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                await _next(context);
                return;
            }

            var userId = context.User.GetUserId();
            var tokenVersion = context.User.GetTokenVersion();
          
            if (userId == null || tokenVersion == null)
                throw new UnauthorizedAccessException("Mã thông báo không hợp lệ.");

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
               throw new UnauthorizedAccessException($"Người dùng không tồn tại : {nameof(user)}");

            if (user.TokenVersion != tokenVersion)
                throw new UnauthorizedAccessException("Mã thông báo đã bị thu hồi.");

            await _next(context);
        }
    }



}
