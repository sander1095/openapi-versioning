using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using url_versioning.Models;

namespace url_versioning.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/scores")]
[ApiVersion(1.0)]
public class ScoresController : ControllerBase
{
    [HttpGet]
    public ActionResult<Scorev1[]> Get()
    {
        return Ok(new Scorev1[]
        {
            new(1, 100),
            new(2, 150)
        });
    }
}
