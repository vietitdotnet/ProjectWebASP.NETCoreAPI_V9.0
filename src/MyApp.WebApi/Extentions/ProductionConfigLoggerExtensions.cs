namespace MyApp.WebApi.Extentions
{
    public static class ProductionConfigLoggerExtensions
    {
        public static void LogProductionConfiguration(this IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            var config = app.ApplicationServices.GetRequiredService<IConfiguration>();

            if (!env.IsProduction())
                return;

            Console.WriteLine("=== PRODUCTION ENV DETECTED ===");
            Console.WriteLine("Environment: " + env.EnvironmentName);
            Console.WriteLine("ConnectionStrings:SQLServerAppDatabase = " + config.GetConnectionString("SQLServerAppDatabase"));
            Console.WriteLine("JWT:Secret = " + config["JWT:Secret"]);
            Console.WriteLine("Authentication:Google:ClientId = " + config["Authentication:Google:ClientId"]);
            Console.WriteLine("================================");
        }
    }
}
