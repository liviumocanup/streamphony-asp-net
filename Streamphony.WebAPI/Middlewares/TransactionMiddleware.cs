using Streamphony.Application.Abstractions;

namespace Streamphony.WebAPI.Middlewares;

public class TransactionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
    {
        var cancellationToken = context.RequestAborted;

        if (context.Request.Method == HttpMethod.Get.Method)
        {
            await _next(context);
            return;
        }

        try
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            await _next(context);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            Console.WriteLine("ROLLBACK");
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
