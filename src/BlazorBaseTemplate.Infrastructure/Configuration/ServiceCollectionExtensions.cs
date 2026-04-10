using Microsoft.Extensions.DependencyInjection;

namespace BlazorBaseTemplate.Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Register infrastructure services here
        // Example: services.AddScoped<IExternalApiClient, ExternalApiClient>();
        return services;
    }
}
