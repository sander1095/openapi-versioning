using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using url_versioning.Models;

namespace url_versioning.Controllers;

[ApiController]
[ApiVersionNeutral]
[Route("api/users")]
public class UsersVersionNeutralController : ControllerBase
{
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        // Delete user logic here
        return NoContent();
    }
}
