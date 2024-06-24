using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Artists.Queries;
using Streamphony.Application.Services;

namespace Streamphony.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(GetAllArtistsHandler).Assembly);
        
        services.AddScoped<ILoggingService, LoggingService>();
        services.AddScoped<IValidationService, ValidationService>();
        
        return services;
    }
}
