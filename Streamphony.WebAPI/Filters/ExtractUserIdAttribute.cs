using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Streamphony.WebAPI.Filters;

public class ExtractUserIdAttribute: Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;
        
        if (user.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                context.HttpContext.Items["UserId"] = userId;
            }
        }

        await next();
    }
}
