using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Infrastructure.Persistence.Contexts;
using Streamphony.Infrastructure.Persistence.Repositories;
using Streamphony.Infrastructure.ServiceProviders.Auth;
using Streamphony.Infrastructure.ServiceProviders.FileStorage;
using Streamphony.Infrastructure.ServiceProviders.Logging;
using Streamphony.Infrastructure.ServiceProviders.Mapping;
using Streamphony.Infrastructure.Validators.CreationDTOs;
using Streamphony.Infrastructure.Validators.DTOs;

namespace Streamphony.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConnectionString = configuration.GetConnectionString("DatabaseConnection");
        var storageConnectionString = configuration.GetConnectionString("StorageConnection");
        
        // Sql Server for Database and Azurite for Blob Storage
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(databaseConnectionString));
        services.AddSingleton<IBlobStorageService>(new AzuriteBlobStorageService(storageConnectionString));
        
        // Repository and UnitOfWork registration
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<ISongRepository, SongRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IPreferenceRepository, PreferenceRepository>();
        services.AddScoped<IBlobRepository, BlobRepository>();
        services.AddScoped<IAlbumArtistRepository, AlbumArtistRepository>();
        
        // Mapping provider with Mapster
        services.AddScoped<IMappingProvider, MapsterProvider>();

        // Logging provider with Serilog
        services.AddSingleton<ILoggingProvider, SerilogProvider>();
        
        // Fluent Validation setup
        services.AddValidatorsFromAssemblyContaining<ArtistCreationDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<ArtistDtoValidator>();
        services.AddFluentValidationAutoValidation();
        
        // Authentication & Authorization setup
        services.AddScoped<IUserManagerProvider, UserManagerProvider>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddJwtAuthentication(configuration);
        
        // File Storage settings
        services.AddScoped<IAudioDurationService, AudioDurationService>();
        services.AddFileStorageSettings(configuration);
        
        return services;
    }
}
