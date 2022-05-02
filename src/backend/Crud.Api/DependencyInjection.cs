using System.Reflection;
using Crud.Application;
using Crud.Data;
using Crud.Shared.Validation;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Crud.Api;

/// <summary>
/// Startup configuration for the application.
/// </summary>
public class DependencyInjection
{
    /// <summary>
    /// Configure <see cref="ConfigurationManager"/>.
    /// </summary>
    public static void ConfigureConfiguration(ConfigurationManager config)
    {
        config.AddEnvironmentVariables();
    }
    
    /// <summary>
    /// Configure <see cref="ConfigureHostBuilder"/>.
    /// </summary>
    public static void ConfigureHost(ConfigureHostBuilder host, ConfigurationManager config)
    {
        host.UseSerilog((_, conf) =>conf 
            .WriteTo.Async(x => x.Console())
            .WriteTo.Async(x =>
                x.Seq(config
                        .GetSection("Seq")
                        .GetValue<string>("Uri"))
                    .MinimumLevel.Debug()));
    }
    
    /// <summary>
    /// Configure <see cref="IServiceCollection"/>.
    /// </summary>
    public static void ConfigureServices(IServiceCollection services, ConfigurationManager config)
    {
        services.AddDbContextPool<ApplicationDbContext>(o =>
            o.UseSqlServer(config.GetConnectionString("Default")));

        services
            .AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllers();
        services.AddMediatR(typeof(MediatorAnchor));
        services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(MediatorAnchor)));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o =>
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
    }
}