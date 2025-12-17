using MyApp.Application.Models.Requests.Auths;
using MyApp.Application.Models.Requests.Registers;
using MyApp.Application.Models.Responses.Logins;
using MyApp.Application.Models.Responses.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Identity
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(string userName, string password);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> RefreshTokenAsync(string token, string refreshToken);
        Task LogoutAsync(string userId);

        Task<ConfirmEmailResponse> ConfirmEmailAsync(string userId, string code);

        Task<LoginResponse> ExternalCallbackLoginAsync();

    }
}
