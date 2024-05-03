using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        if (context.Exception is ValidationException validationException)
        {
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            response.StatusPhrase = HttpStatusCode.BadRequest.ToString();
            response.Errors.AddRange(validationException.Errors.Select(error => error.ErrorMessage));
        }
        else
        {
            response.Errors.Add(context.Exception.Message);
        }

        context.Result = new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }
}