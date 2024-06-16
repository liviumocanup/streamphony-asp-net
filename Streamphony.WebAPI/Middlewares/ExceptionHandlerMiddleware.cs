using System.Net;
using System.Text.Json;
using FluentValidation;
using Streamphony.Application.Exceptions;
using Streamphony.WebAPI.Common.Models;

namespace Streamphony.WebAPI.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = new ErrorResponse
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            StatusPhrase = HttpStatusCode.InternalServerError.ToString(),
            Timestamp = DateTime.UtcNow
        };

        switch (exception)
        {
            case ValidationException validationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.StatusPhrase = HttpStatusCode.BadRequest.ToString();
                response.Errors.AddRange(validationException.Errors.Select(error => error.ErrorMessage));
                break;
            case NotFoundException notFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.StatusPhrase = HttpStatusCode.NotFound.ToString();
                response.Errors.Add(notFoundException.Message);
                break;
            case DuplicateException duplicateException:
                response.StatusCode = (int)HttpStatusCode.Conflict;
                response.StatusPhrase = HttpStatusCode.Conflict.ToString();
                response.Errors.Add(duplicateException.Message);
                break;
            case UnauthorizedException unauthorizedException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response.StatusPhrase = HttpStatusCode.Unauthorized.ToString();
                response.Errors.Add(unauthorizedException.Message);
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Errors.Add(exception.Message);
                break;
        }

        context.Response.StatusCode = response.StatusCode;
        var result = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(result);
    }
}
