using System.Reflection;
using Streamphony.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Infrastructure.Persistence.Repositories;
using Streamphony.Application.Interfaces;
using Streamphony.Infrastructure.Logging;
using System.IO.Abstractions;
using Streamphony.Application.Common.Mappings;
using Streamphony.Application.Services;
using MediatR;
using Streamphony.Application.App.Users.Queries;
using Streamphony.Application.Users.Queries;
using Streamphony.Application.App.Users.Commands;

namespace Streamphony.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository), typeof(Repository));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddScoped<ILoggingService, FileLoggingService>();

            services.AddMediatR(typeof(GetAllUsersHandler).Assembly);
            services.AddMediatR(typeof(GetUserById).Assembly);
            services.AddMediatR(typeof(CreateUser).Assembly);

            return services;
        }
    }
}
