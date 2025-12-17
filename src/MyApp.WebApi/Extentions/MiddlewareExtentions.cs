using Microsoft.AspNetCore.Builder;
using MyApp.WebApi.Middlewares;

namespace MyApp.WebApi.Extentions
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder HardCodedTokenMiddlewareExtention(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HardCodedTokenMiddleware>();
        }

        public static IApplicationBuilder RevokedTokenMiddlewareExtention(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RevokedTokenMiddleware>();
        }

    }
}
