using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Streamphony.WebAPI.Common.Models;

namespace Streamphony.WebAPI.Filters;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var apiError = new ErrorResponse
            {
                StatusCode = 400,
                StatusPhrase = "Bad Request",
                Timestamp = DateTime.Now
            };

            foreach (var error in context.ModelState)
            {
                foreach (var inner in error.Value.Errors)
                {
                    apiError.Errors.Add(inner.ErrorMessage);
                }
            }

            context.Result = new BadRequestObjectResult(apiError);
        }
    }
}