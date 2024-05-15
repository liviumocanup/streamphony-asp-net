using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Mapping;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.Abstractions.Services;
using Streamphony.Application.App.Artists.Queries;
using Streamphony.Application.Services;
using Streamphony.Infrastructure.Logging;
using Streamphony.Infrastructure.Mapping;
using Streamphony.Infrastructure.Persistence.Contexts;
using Streamphony.Infrastructure.Persistence.Repositories;
using Streamphony.Infrastructure.Validators.CreationDTOs;
using Streamphony.Infrastructure.Validators.DTOs;
using Streamphony.Infrastructure.Auth;

namespace Streamphony.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddMediatR(typeof(GetAllArtistsHandler).Assembly);

        // Repository and UnitOfWork registration
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<ISongRepository, SongRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IPreferenceRepository, PreferenceRepository>();
        services.AddTransient<IValidationService, ValidationService>();

        // Mapping provider with Mapster
        services.AddScoped<IMappingProvider, MapsterProvider>();

        // Logging provider with Serilog
        services.AddSingleton<ILoggingProvider, SerilogProvider>();

        // Application Services registration
        services.AddScoped<ILoggingService, LoggingService>();
        services.AddScoped<IValidationService, ValidationService>();

        // Fluent Validation setup
        services.AddValidatorsFromAssemblyContaining<ArtistCreationDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<ArtistDtoValidator>();
        services.AddFluentValidationAutoValidation();

        // Auth
        services.AddScoped<IUserManagerProvider, UserManagerProvider>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddJwtAuthentication(configuration);

        // Serilog configuration
        services.AddSerilog((services, lc) => lc
            .ReadFrom.Configuration(configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext(),
            true);

        return services;
    }
}
