using Microsoft.EntityFrameworkCore;
using Streamphony.Application.Common.Mappings;
using Streamphony.Application.Interfaces;
using Streamphony.Application.Interfaces.Repositories;
using Streamphony.Application.Services;
using Streamphony.Infrastructure.Persistence.Context;
using Streamphony.Infrastructure.Persistence.Repositories;

namespace Streamphony.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository), typeof(Repository));
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<ILoggingService, FileLoggingService>();

            return services;
        }
    }
}