using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using queryheader_versioning_openapi.Models;

namespace queryheader_versioning_openapi.Controllers;

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
            new UserV1(1, "John Doe", "johndoe@example.com"),
            new UserV1(2, "Alice Dewett", "alice@example.com"),
        });
    }

    [HttpGet]
    [MapToApiVersion(2.0)]
    public ActionResult<UserV2[]> GetV2()
    {
        return Ok(new[]
        {
            new UserV2(1, "John Doe", "johndoe@example.com", new DateOnly(1990, 1, 1)),
            new UserV2(2, "Alice Dewett", "alice@example.com", new DateOnly(1992, 2, 2)),
        });
    }

    [HttpDelete("{id:int}")]
    [ApiVersionNeutral]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete(int id)
    {
        // Delete user logic here
        return NoContent();
    }
}
