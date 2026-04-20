namespace BlazorBaseTemplate.Infrastructure.Configuration;

using Microsoft.Extensions.DependencyInjection;
using BlazorBaseTemplate.Application.Interfaces;
using BlazorBaseTemplate.Application.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISampleDataService, SampleDataService>();
        return services;
    }
}
