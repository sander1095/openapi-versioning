using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using url_versioning.Models;

namespace url_versioning.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion(1.0)]
public class UsersController : ControllerBase
{
    [HttpGet]
    public ActionResult<Userv1[]> Get()
    {
        return Ok(new[]
        {
            new Userv1(1, "John Doe", "johndoe@example.com"),
            new Userv1(2, "Alice Dewett", "alice@example.com"),
        });
    }
}
