using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using queryheader_versioning.Models;

namespace queryheader_versioning.Controllers;

[ApiController]
[Route("api/users")]
[ApiVersion(1.0)]
[ApiVersion(2.0)]
public class UsersController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion(1.0)]
    public IActionResult GetV1()
    {
        return Ok(new[]
        {
            new Userv1(1, "John Doe", "johndoe@example.com"),
            new Userv1(2, "Alice Dewett", "alice@example.com"),
        });
    }

    [HttpGet]
    [MapToApiVersion(2.0)]
    public IActionResult GetV2()
    {
        return Ok(new[]
        {
            new Userv2(1, "John Doe", "johndoe@example.com", new DateOnly(1990, 1, 1)),
            new Userv2(2, "Alice Dewett", "alice@example.com", new DateOnly(1992, 2, 2)),
        });
    }

    [HttpDelete("{id:int}")]
    [ApiVersionNeutral]
    public IActionResult Delete(int id)
    {
        // Delete user logic here
        return NoContent();
    }
}
