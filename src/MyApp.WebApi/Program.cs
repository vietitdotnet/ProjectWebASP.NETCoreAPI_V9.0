using MyApp.Application;
using MyApp.Domain;
using MyApp.Infrastructure;
using MyApp.Infrastructure.Extentions;
using MyApp.WebApi.Exceptions.Extentions;
using MyApp.WebApi.Extentions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureInfrastructure();

builder.Services.ConfigureAuthentication(builder.Configuration);


builder.Services.ConfigureApplication();

builder.Services.ExternalLoginService(builder.Configuration);

builder.Services.AddUnifiedExceptionHandling();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.LogProductionConfiguration();
app.UseHttpsRedirection();

/*await app.Services.CreateAdmin();*/

/*app.HardCodedTokenMiddlewareExtention();*/

app.UseExceptionHandler();

app.UseAuthentication();

app.RevokedTokenMiddlewareExtention();

app.UseAuthorization();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

app.Run();
