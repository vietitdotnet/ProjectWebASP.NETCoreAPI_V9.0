using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Extentions
{
    public static class AuthenticationServiceExtensions
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtSection = config.GetSection("JWT");

            var secretKey = jwtSection["Secret"];
          
            var issuer = jwtSection["ValidIssuer"];
            var audience = jwtSection["ValidAudience"];

            // Cấu hình Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Đặt true khi production
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true, // Quan trọng: kiểm tra thời hạn token
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero // Không cho phép sai lệch thời gian 5 phút mặc định
                };

                options.AddCustomJwtEvents();
            });

            // Cấu hình Authorization (phân quyền chi tiết ở đây nếu muốn)
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                    policy.RequireRole("Admin", "Administrator"));

                options.AddPolicy("Employee", policy =>
                    policy.RequireRole("Admin", "Administrator", "Employee"));

                options.AddPolicy("Customer", policy =>
                    policy.RequireRole("Admin", "Administrator", "Employee", "Customer"));
            });
        }
    }

}
