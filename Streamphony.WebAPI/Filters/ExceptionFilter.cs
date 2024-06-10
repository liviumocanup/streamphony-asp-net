using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Streamphony.Application.Exceptions;
using Streamphony.WebAPI.Common.Models;

namespace Streamphony.WebAPI.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var response = new ErrorResponse
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            StatusPhrase = HttpStatusCode.InternalServerError.ToString(),
            Timestamp = DateTime.UtcNow
        };

        switch (context.Exception)
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
                response.Errors.Add(context.Exception.Message);
                break;
        }

        context.Result = new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}
