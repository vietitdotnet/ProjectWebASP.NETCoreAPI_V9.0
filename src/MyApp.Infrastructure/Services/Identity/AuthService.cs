using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyApp.Application.Core.Models;
using MyApp.Application.Interfaces.Identity;
using MyApp.Application.Interfaces.Jwt;
using MyApp.Application.Models.Requests.Auths;
using MyApp.Application.Models.Requests.Registers;
using MyApp.Application.Models.Responses.Logins;
using MyApp.Application.Models.Responses.Registers;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Exceptions.APIRoutes;
using MyApp.Infrastructure.Constants;
using MyApp.Infrastructure.Models;
using MyApp.Infrastructure.Models.Enums;
using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;


namespace MyApp.Infrastructure.Services.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        

        private readonly ILogger<AuthService> _logger;
        public AuthService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtService jwtService,
            IConfiguration configuration,   
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _configuration = configuration;
           
            _logger = logger;
        }

        public async Task<LoginResponse> LoginAsync(string userName, string password)
        {
            // Cho phép login bằng username hoặc email
            var user = await _userManager.FindByNameAsync(userName)
                    ?? await _userManager.FindByEmailAsync(userName);

            if (user == null)
                throw new NotFoundException("Tài khoản không tồn tại.");

            // Kiểm tra nếu bị khóa
            if (await _userManager.IsLockedOutAsync(user))
                throw new RedirectRequestException("Tài khoản bị khóa", RedirectRequest.LockoutRequired);

            // Kiểm tra provider
            if (user.Provider != AuthProvider.Local)
                throw new BadRequestException($"Tài khoản này chỉ được đăng nhập bằng {user.Provider}.");

            // Xác thực mật khẩu (lockoutOnFailure = true)
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);

            if (!result.Succeeded)
                throw new BadRequestException("Sai mật khẩu.");

            // Lấy role sau khi đã xác thực
            var roles = await _userManager.GetRolesAsync(user);

            // Chỉ cho Admin + Employee
            var isManager = roles.Any(r =>
                r.Equals(AuthRoler.Admin.ToString(), StringComparison.OrdinalIgnoreCase) ||
                r.Equals(AuthRoler.Employee.ToString(), StringComparison.OrdinalIgnoreCase));

            if (!isManager)
                throw new BadRequestException("Không có quyền đăng nhập.");

            // Sinh token
            var accessToken = _jwtService.GenerateAccessToken(user, roles);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Lưu refresh token vào DB
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            return new LoginResponse
            {
                Success = true,
                Message = "Đăng nhập thành công.",
                Data = new LoginResponseData
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FullName = $"{user.FirstName} {user.LastName}".Trim(),
                    TokenVersion = user.TokenVersion,
                    Roles = roles.ToList(),
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresIn = int.Parse(_configuration["JWT:AccessTokenValidityInSeconds"])
                }
            };
        }


        public async Task LogoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("Người dùng không tồn tại.");

            // Xóa refresh token
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            user.TokenVersion += 1;

            await _userManager.UpdateAsync(user);
        }


        public async Task<LoginResponse> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(token);

            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            var tokenVersionClaim = principal.FindFirst(UserClaimTypes.TokenVersion)?.Value;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null
                || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiryTime <= DateTime.UtcNow
                || tokenVersionClaim != user.TokenVersion.ToString())
            {
                throw new UnauthorizedAccessException("Refresh token không hợp lệ hoặc đã hết hạn.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var newAccessToken = _jwtService.GenerateAccessToken(user, roles);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            return new LoginResponse
            {
                Success = true,
                Message = "Refresh token thành công",
                Data = new LoginResponseData
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FullName = $"{user.FirstName} {user.LastName}".Trim(),
                    TokenVersion = user.TokenVersion,
                    Roles = roles.ToList(),
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    ExpiresIn = int.Parse(_configuration["JWT:AccessTokenValidityInSeconds"])
                }
            };
        }


        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _userManager.FindByNameAsync(request.UserName) != null)
                throw new BadRequestException("Tên đăng nhập đã tồn tại");

            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Provider = AuthProvider.Local,
                TokenVersion = 1 // cần nếu dùng middleware check tokenVersion
            };

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                user.Email = request.Email;
                user.EmailConfirmed = false;
            }

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new BadRequestException(string.Join("; ", result.Errors.Select(e => e.Description)));

            // Thêm role mặc định nếu muốn
            await _userManager.AddToRoleAsync(user, "User");
            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = _jwtService.GenerateAccessToken(user, roles);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return new RegisterResponse
            {
                Success = true,
                Message = "Đăng ký thành công",
                Data = new RegisterResponseData
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = request.Email,
                    FullName = $"{user.FirstName} {user.LastName}".Trim(),
                    TokenVersion = user.TokenVersion,
                    Roles = roles.ToList(),
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresIn = int.Parse(_configuration["JWT:AccessTokenValidityInSeconds"])
                }
            };
        }


        public async Task<LoginResponse> ExternalCallbackLoginAsync()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
                throw new ArgumentNullException(nameof(info));

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var userName = info.Principal.FindFirstValue(ClaimTypes.Email) ?? $"{info.LoginProvider}_{info.ProviderKey}";       
            var fullName = info.Principal.FindFirstValue(ClaimTypes.Name) ?? "";

            // Kiểm tra user đã tồn tại chưa
            var user = await _userManager.FindByEmailAsync(email);
            bool isNewUser = false;

            if (user == null)
            {
                isNewUser = true;
                user = new AppUser
                {
                    UserName = userName,
                    Email = email,
                    FirstName =  ExtractFirstName(fullName),
                    LastName = ExtractLastName(fullName),
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    throw new BadRequestException(string.Join("; ", createResult.Errors.Select(x => x.Description)));
                }
            }

            if (!string.IsNullOrEmpty(user.Email))
                    user.EmailConfirmed = true;

            // Gắn external login
            var loginResult = await _userManager.AddLoginAsync(user, info);
            // Nếu login đã tồn tại thì AddLoginAsync sẽ fail → bỏ qua lỗi này

            // Lấy roles
            var roles = await _userManager.GetRolesAsync(user);

            // Tăng TokenVersion để vô hiệu hoá token cũ
            user.TokenVersion++;

            var accessToken = _jwtService.GenerateAccessToken(user, roles);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Lưu refresh token vào DB
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            return new LoginResponse
            {
                Success = true,
                Message = isNewUser ? "Tạo mới và đăng nhập thành công." : "Đăng nhập thành công.",
                Data = new LoginResponseData
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FullName = fullName,
                    TokenVersion = user.TokenVersion,
                    Roles = roles.ToList(),
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresIn = int.Parse(_configuration["JWT:AccessTokenValidityInSeconds"])
                }
            };
        }

        private string ExtractFirstName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return string.Empty;
            var parts = fullName.Trim().Split(' ');
            return parts.Length > 0 ? parts[0] : fullName;
        }

        private string ExtractLastName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return string.Empty;
            var parts = fullName.Trim().Split(' ');
            return parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : "";
        }

        public async Task<ConfirmEmailResponse> ConfirmEmailAsync(string userId, string code)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadRequestException("UserId không hợp lệ.");

            if (string.IsNullOrWhiteSpace(code))
                throw new BadRequestException("Mã xác thực không hợp lệ.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("Người dùng không tồn tại.");

            string decodedCode;

            try
            {
                decodedCode = Encoding.UTF8.GetString(
                    WebEncoders.Base64UrlDecode(code)
                );
            }
            catch
            {
                throw new BadRequestException("Mã xác thực không hợp lệ hoặc đã bị thay đổi.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, decodedCode);

            if (!result.Succeeded)
                throw new BadRequestException(
                    string.Join("; ", result.Errors.Select(e => e.Description))
                );

            return new ConfirmEmailResponse
            {
                Success = true,
                Message = "Email confirmed successfully."
            };
        }

    }
}
