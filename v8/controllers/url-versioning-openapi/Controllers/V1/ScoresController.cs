using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using url_versioning_openapi.Models;

namespace url_versioning_openapi.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/scores")]
[ApiVersion(1.0)]
public class ScoresController : ControllerBase
{
    [HttpGet]
    public ActionResult<ScoreV1[]> Get()
    {
        return Ok(new ScoreV1[]
        {
            new(1, 100),
            new(2, 150)
        });
    }
}
