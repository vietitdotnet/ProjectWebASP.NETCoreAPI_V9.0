using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Application;
using MyApp.Application.Core.Services;
using MyApp.Application.Interfaces.Identity;
using MyApp.Application.Interfaces.Jwt;
using MyApp.Domain.Core.Repositories;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Models;
using MyApp.Infrastructure.Repositories;
using MyApp.Infrastructure.Services;
using MyApp.Infrastructure.Services.Identity;
using MyApp.Infrastructure.Services.JWT;

namespace MyApp.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<MyAppDbContext>(options =>
                options.UseSqlServer("name=ConnectionStrings:SQLServerAppDatabase",
                x => x.MigrationsAssembly("MyApp.Infrastructure")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(
                typeof(IBaseRepositoryAsync<>),
                typeof(BaseRepositoryAsync<>)
            );

            services.AddAutoMapper(
                typeof(ApplicationAssemblyMarker).Assembly
              
            );

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<MyAppDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddSingleton<ILoggerService, LoggerService>();
            
        }
        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<MyAppDbContext>>();

            using (var dbContext = new MyAppDbContext(dbContextOptions))
            {
                dbContext.Database.Migrate();
            }
        }
        public static async Task CreateAdmin(this IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var services = scope.ServiceProvider;
                await SeedData.InitializeAsync(services);              
            }
        }

       
    }
}