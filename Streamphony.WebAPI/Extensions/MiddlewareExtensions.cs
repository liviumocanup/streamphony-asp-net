using Streamphony.WebAPI.Middlewares;

namespace Streamphony.WebAPI.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }

    public static IApplicationBuilder UseRequestTiming(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestTimerMiddleware>();
    }

    public static IApplicationBuilder UseDbTransaction(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TransactionMiddleware>();
    }
}
