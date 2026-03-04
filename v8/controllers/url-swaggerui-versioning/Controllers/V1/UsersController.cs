using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using url_swaggerui_versioning.Models;

namespace url_swaggerui_versioning.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion(1.0)]
public class UsersController : ControllerBase
{
    [HttpGet]
    public ActionResult<UserV1[]> Get()
    {
        return Ok(new[]
        {
            new UserV1(1, "John Doe", "johndoe@example.com"),
            new UserV1(2, "Alice Dewett", "alice@example.com"),
        });
    }
}
