using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO.Abstractions;
using Streamphony.Application.Abstractions;
using Streamphony.Application.Abstractions.Logging;
using Streamphony.Application.Abstractions.Repositories;
using Streamphony.Application.App.Users.Queries;
using Streamphony.Application.Common.Mappings;
using Streamphony.Infrastructure.Logging;
using Streamphony.Infrastructure.Persistence.Contexts;
using Streamphony.Infrastructure.Persistence.Repositories;

namespace Streamphony.WebAPI.Extensions
{
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

            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddScoped<ILoggingService, FileLoggingService>();


            return services;
        }
    }
}
