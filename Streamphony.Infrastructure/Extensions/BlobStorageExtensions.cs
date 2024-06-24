using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Streamphony.Application.Abstractions.Configurations;
using Streamphony.Infrastructure.Configurations;

namespace Streamphony.Infrastructure.Extensions;

public static class BlobStorageExtensions
{
    public static IServiceCollection AddFileStorageSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<FileStorageSettings>(configuration.GetSection("FileStorageSettings"));
        
        services.AddSingleton<IFileStorageSettings>(sp =>
            sp.GetRequiredService<IOptions<FileStorageSettings>>().Value);
        
        return services;
    }
}
