using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using url_versioning_openapi_swaggerui.Models;

namespace url_versioning_openapi_swaggerui.Controllers.V2;

[ApiController]
[Route("api/v{version:apiVersion}/scores")]
[ApiVersion(2.0)]
public class ScoresController : ControllerBase
{
    [HttpGet]
    public ActionResult<ScoreV2[]> Get()
    {
        return Ok(new ScoreV2[]
        {
            new(1, 100, DateTimeOffset.UtcNow.AddDays(-2)),
            new(2, 150, DateTimeOffset.UtcNow.AddDays(-1))
        });
    }
}
