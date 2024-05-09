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
using Streamphony.Application.App.Users.Queries;
using Streamphony.Application.Services;
using Streamphony.Infrastructure.Logging;
using Streamphony.Infrastructure.Mapping;
using Streamphony.Infrastructure.Persistence.Contexts;
using Streamphony.Infrastructure.Persistence.Repositories;
using Streamphony.Infrastructure.Validation.Validators.CreationDTOs;
using Streamphony.Infrastructure.Validation.Validators.DTOs;

namespace Streamphony.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddMediatR(typeof(GetAllUsersHandler).Assembly);

        // Repository and UnitOfWork registration
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISongRepository, SongRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IUserPreferenceRepository, UserPreferenceRepository>();
        services.AddTransient<IValidationService, ValidationService>();

        // Mapping provider with Mapster
        services.AddScoped<IMappingProvider, MapsterProvider>();

        // Logging provider with Serilog
        services.AddSingleton<ILoggingProvider, SerilogProvider>();

        // Application Services registration
        services.AddScoped<ILoggingService, LoggingService>();
        services.AddScoped<IValidationService, ValidationService>();

        // Fluent Validation setup
        services.AddValidatorsFromAssemblyContaining<UserCreationDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();
        services.AddFluentValidationAutoValidation();

        // Serilog configuration
        services.AddSerilog((services, lc) => lc
            .ReadFrom.Configuration(configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext(),
            true);

        return services;
    }
}
