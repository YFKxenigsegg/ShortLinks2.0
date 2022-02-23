using FluentMigrator.Runner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ShortLinks.Persistence.Migrations;
[ApiController]
[Route("api/[controller]")]
public class RollBackController : ControllerBase
{
    private readonly ILogger<RollBackController> _logger;
    private readonly IMigrationRunner _migrationRunner;

    public RollBackController(
        ILogger<RollBackController> logger
        , IMigrationRunner migrationRunner
        ) => (_logger, _migrationRunner) = (logger, migrationRunner);

    [HttpGet("Rollback")]
    public ActionResult RollBack()
    {
        _logger.LogInformation("Rollback transaction");
        _migrationRunner.Rollback(1);
        return Ok();
    }
}
