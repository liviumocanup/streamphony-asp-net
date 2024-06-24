using System.Diagnostics;

namespace Streamphony.WebAPI.Middlewares;

public class RequestTimerMiddleware(RequestDelegate next, ILogger<RequestTimerMiddleware> logger)
{
    private readonly ILogger<RequestTimerMiddleware> _logger = logger;
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();
        _logger.LogInformation("Request [{Method} {Url}] completed in {Duration}ms",
            context.Request.Method,
            context.Request.Path.Value,
            stopwatch.ElapsedMilliseconds);
    }
}
