using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using queryheader_versioning_openapi_swaggerui.Models;

namespace queryheader_versioning_openapi_swaggerui.Controllers;

[ApiController]
[Route("api/scores")]
[ApiVersion(1.0)]
[ApiVersion(2.0)]
public class ScoresController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion(1.0)]
    public ActionResult<ScoreV1[]> GetV1()
    {
        return Ok(new ScoreV1[]
        {
            new(1, 100),
            new(2, 150)
        });
    }

    [HttpGet]
    [MapToApiVersion(2.0)]
    public ActionResult<ScoreV2[]> GetV2()
    {
        return Ok(new ScoreV2[]
        {
            new(1, 100, DateTimeOffset.UtcNow.AddDays(-2)),
            new(2, 150, DateTimeOffset.UtcNow.AddDays(-1))
        });
    }
}
