

using MyApp.WebApi.Exceptions.Handler;

namespace MyApp.WebApi.Exceptions.Extentions
{
    
    public static class ExceptionHandlingServiceExtensions
    {
        public static IServiceCollection AddUnifiedExceptionHandling(this IServiceCollection services)
        {
            // 1️⃣ Bật ProblemDetails (chuẩn RFC 7807)
            services.AddProblemDetails();

            // 2️⃣ Cấu hình response cho validation tự động
            services.AddUnifiedValidationResponse();

            // 3️⃣ Đăng ký các ExceptionHandler
            services.AddExceptionHandler<AuthorizeExceptionHandler>();
            services.AddExceptionHandler<ForbiddenExceptionHandler>();
            services.AddExceptionHandler<BadRequestExceptionHandler>();
            services.AddExceptionHandler<NotFoundExceptionHandler>();
            services.AddExceptionHandler<RedirectRequestHandler>();
            services.AddExceptionHandler<DatabaseExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
    

}
