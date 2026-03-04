using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using url_swaggerui_versioning.Models;

namespace url_swaggerui_versioning.Controllers.V2;

[ApiController]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion(2.0)]
public class UsersController : ControllerBase
{
    [HttpGet]
    public ActionResult<UserV2[]> Get()
    {
        return Ok(new[]
        {
            new UserV2(1, "John Doe", "johndoe@example.com", new DateOnly(1990, 1, 1)),
            new UserV2(2, "Alice Dewett", "alice@example.com", new DateOnly(1992, 2, 2)),
        });
    }
}
