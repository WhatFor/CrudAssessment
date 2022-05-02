using System;
using System.IO;
using System.Threading.Tasks;
using Crud.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;

namespace Crud.IntegrationTests;

/// <summary>
/// Base for all integration tests.
/// </summary>
public class TestBase
{
    private static IServiceScopeFactory? _scopeFactory;
    private static ConfigurationManager? _configuration;
    
    /// <summary>
    /// Run before each test.
    /// </summary>
    protected TestBase()
    {
        if (_configuration is null)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();
            
            var configuration = builder.Build();
            
            _configuration = new ConfigurationManager();
            _configuration.AddConfiguration(configuration);
        }
        
        if (_scopeFactory is null)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            DependencyInjection.ConfigureServices(services, _configuration);
            
            _scopeFactory = services
                .BuildServiceProvider()
                .GetRequiredService<IServiceScopeFactory>();
        }
        
        var checkpoint = new Checkpoint
        {
            TablesToIgnore = new Table[] { "__EFMigrationsHistory" }
        };
        
        checkpoint
            .Reset(_configuration.GetConnectionString("Default"))
            .GetAwaiter()
            .GetResult();
    }
    
    protected static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        if (_scopeFactory is null)
            throw new InvalidOperationException($"{nameof(TestBase)} is broken.");
        
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
        return await mediator.Send(request);
    }
}