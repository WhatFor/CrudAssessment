using Crud.Api;
using Crud.Api.Middleware;
using Crud.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

DependencyInjection.ConfigureConfiguration(builder.Configuration);
DependencyInjection.ConfigureHost(builder.Host, builder.Configuration);
DependencyInjection.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

app
    .UseMiddleware<ExceptionHandlerMiddleware>()
    .UseSwagger()
    .UseSwaggerUI()
    .UseHealthChecks("/health")
    .UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();

// Ensure the database is created and migrated on startup.
using (var scope = app.Services.CreateScope())
{
    await scope
        .ServiceProvider
        .GetRequiredService<ApplicationDbContext>()
        .Database
        .MigrateAsync();
}

app.Run();