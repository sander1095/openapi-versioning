using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using queryheader_versioning.Models;

namespace queryheader_versioning.Controllers;

[ApiController]
[Route("api/scores")]
[ApiVersion(1.0)]
[ApiVersion(2.0)]
public class ScoresController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion(1.0)]
    public ActionResult<Scorev1[]> GetV1()
    {
        return Ok(new Scorev1[]
        {
            new(1, 100),
            new(2, 150)
        });
    }

    [HttpGet]
    [MapToApiVersion(2.0)]
    public ActionResult<Scorev2[]> GetV2()
    {
        return Ok(new Scorev2[]
        {
            new(1, 100, DateTimeOffset.UtcNow.AddDays(-2)),
            new(2, 150, DateTimeOffset.UtcNow.AddDays(-1))
        });
    }
}
