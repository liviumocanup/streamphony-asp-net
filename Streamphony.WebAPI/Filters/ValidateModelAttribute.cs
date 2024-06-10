using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Streamphony.WebAPI.Common.Models;

namespace Streamphony.WebAPI.Filters;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;
        var apiError = new ErrorResponse
        {
            StatusCode = 400,
            StatusPhrase = "Bad Request",
            Timestamp = DateTime.Now
        };

        apiError.Errors.AddRange(
            context.ModelState.SelectMany(e => e.Value!.Errors.Select(error => error.ErrorMessage)));

        context.Result = new BadRequestObjectResult(apiError);
    }
}
