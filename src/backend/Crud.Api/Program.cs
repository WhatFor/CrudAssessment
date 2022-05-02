using System.Reflection;
using Crud.Data;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Host.UseSerilog((_, config) => config
    .WriteTo.Async(x => x.Console())
    .WriteTo.Async(x =>
        x.Seq(builder
                .Configuration
                .GetSection("Seq")
                .GetValue<string>("Uri"))
            .MinimumLevel.Debug()));

builder.Services.AddDbContextPool<ApplicationDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "V1",
        Title = "Crud API",
        Description = "An ASP.NET Core 6 Web API serving simple CRUD operations. Enjoy!",
    });
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app
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