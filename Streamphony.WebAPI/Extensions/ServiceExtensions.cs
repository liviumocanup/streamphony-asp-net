using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO.Abstractions;
using FluentValidation;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.App.Users.Queries;
using Streamphony.Infrastructure.Logging;
using Streamphony.Infrastructure.Persistence.Contexts;
using Streamphony.Infrastructure.Persistence.Repositories;
using Streamphony.Infrastructure.Persistence.Validators.DTOs;
using Streamphony.Infrastructure.Persistence.Validators.CreationDTOs;

namespace Streamphony.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddMediatR(typeof(GetAllUsersHandler).Assembly);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISongRepository, SongRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IUserPreferenceRepository, UserPreferenceRepository>();

        // Set up for AutoMapper
        // services.AddAutoMapper(typeof(MappingProfile).Assembly);
        // services.AddScoped<Application.Abstractions.Mapping.IMapper, AutoMapperService>();

        // Set up for Mapster
        services.AddScoped<Application.Abstractions.Mapping.IMapper, Infrastructure.Mapping.MapsterMapper>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddScoped<ILoggingService, FileLoggingService>();

        services.AddSingleton<ILoggerManager, SerilogManager>();

        services.AddValidatorsFromAssemblyContaining<UserCreationDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();
        services.AddFluentValidationAutoValidation();

        services.AddSerilog((services, lc) => lc
            .ReadFrom.Configuration(configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext());

        return services;
    }
}
