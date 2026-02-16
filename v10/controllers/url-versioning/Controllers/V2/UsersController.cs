using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using url_versioning.Models;

namespace url_versioning.Controllers.V2;

[ApiController]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion(2.0)]
public class UsersController : ControllerBase
{
    [HttpGet]
    public ActionResult<Userv2[]> Get()
    {
        return Ok(new[]
        {
            new Userv2(1, "John Doe", "johndoe@example.com", new DateOnly(1990, 1, 1)),
            new Userv2(2, "Alice Dewett", "alice@example.com", new DateOnly(1992, 2, 2)),
        });
    }
}
