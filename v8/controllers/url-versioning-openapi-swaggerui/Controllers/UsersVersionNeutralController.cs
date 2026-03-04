using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using url_versioning_openapi_swaggerui.Models;

namespace url_versioning_openapi_swaggerui.Controllers;

[ApiController]
[ApiVersionNeutral]
[Route("api/users")]
[Tags("Users")]
public class UsersVersionNeutralController : ControllerBase
{
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete(int id)
    {
        // Delete user logic here
        return NoContent();
    }
}
