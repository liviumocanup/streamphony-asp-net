using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Streamphony.WebAPI.Controllers;

[ApiController]
[Authorize]
public abstract class AppBaseController : ControllerBase
{
}
