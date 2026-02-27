using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using minimal_setup_no_openapi.Models;

namespace minimal_setup_no_openapi.Controllers;

[ApiController]
[Route("api/users")]
[ApiVersion(1.0)]
[ApiVersion(2.0)]
public class UsersController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion(1.0)]
    public ActionResult<UserV1[]> GetV1()
    {
        return Ok(new[]
        {
            new UserV1(1, "John Doe"),
            new UserV1(2, "Alice Dewett"),
        });
    }

    [HttpGet]
    [MapToApiVersion(2.0)]
    public ActionResult<UserV2[]> GetV2()
    {
        return Ok(new[]
        {
            new UserV2(1, "John Doe", new DateOnly(1990, 1, 1)),
            new UserV2(2, "Alice Dewett", new DateOnly(1992, 2, 2)),
        });
    }
}
