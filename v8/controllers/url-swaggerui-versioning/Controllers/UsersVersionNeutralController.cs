using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using url_swaggerui_versioning.Models;

namespace url_swaggerui_versioning.Controllers;

[ApiController]
[ApiVersionNeutral]
[Route("api/users")]
public class UsersVersionNeutralController : ControllerBase
{
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        // Delete user logic here
        return NoContent();
    }
}
