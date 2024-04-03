using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO.Abstractions;
using Streamphony.Domain.Models;
using Streamphony.Application.Interfaces;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Application.Common.Mappings;
using Streamphony.Application.Logging;
using Streamphony.Infrastructure.Persistence.Context;
using Streamphony.Infrastructure.Persistence.Repositories;
using Streamphony.Infrastructure.Logging;
using Streamphony.Application.App.Users.Queries;

namespace Streamphony.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Song>, Repository<Song>>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddScoped<ILoggingService, FileLoggingService>();

            services.AddMediatR(typeof(GetAllUsersHandler).Assembly);

            return services;
        }
    }
}
