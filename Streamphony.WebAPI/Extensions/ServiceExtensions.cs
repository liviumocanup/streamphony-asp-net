using Microsoft.AspNetCore.Authorization;
using Serilog;
using Serilog.Events;
using Streamphony.Application.Extensions;
using Streamphony.Infrastructure.Extensions;
using Streamphony.WebAPI.Authorization;

namespace Streamphony.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .CreateBootstrapLogger();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwagger();
        builder.Services.AddCorsPolicy();
        builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();

        // Serilog configuration
        builder.Services.AddSerilog((serv, lc) => lc
                .ReadFrom.Configuration(builder.Configuration)
                .ReadFrom.Services(serv)
                .Enrich.FromLogContext(),
            true);
    }
}
