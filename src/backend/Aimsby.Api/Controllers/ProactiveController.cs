using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aimsby.Api.Infrastructure;
using Aimsby.Application.Proactive;
using Aimsby.Domain.Entities;

namespace Aimsby.Api.Controllers;

[ApiController]
[Route("api/proactive")]
[Authorize]
[RequireFeature(InstanceConfig.Modules.Proactive)]
public class ProactiveController(IProactiveService proactive) : ControllerBase
{
    /// <summary>How many recent runs the history shows.</summary>
    private const int RunsShown = 30;

    /// <summary>Recent proactive runs, newest first — what was sent and when.</summary>
    [HttpGet("runs")]
    public async Task<IActionResult> Runs(CancellationToken ct) => Ok(await proactive.GetRecentRunsAsync(RunsShown, ct));

    /// <summary>
    /// Runs a job now, ignoring its schedule. Useful for previewing a brief and
    /// for verification. Pass force=false to respect the once-per-day rule.
    /// </summary>
    [HttpPost("run/{jobName}")]
    public async Task<IActionResult> Run(string jobName, [FromQuery] bool force = true, CancellationToken ct = default)
    {
        if (!ProactiveJobs.All.Contains(jobName))
            return BadRequest(new
            {
                error = $"Unknown job '{jobName}'. Valid jobs: {string.Join(", ", ProactiveJobs.All)}.",
                code = "unknown_proactive_job",
            });

        return Ok(await proactive.RunJobAsync(jobName, force, ct));
    }
}
