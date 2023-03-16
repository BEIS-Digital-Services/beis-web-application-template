
namespace Beis.Ebss.ApplicationPortal.Web.Controllers;

public class HeartbeatController : Controller
{
    private readonly ILogger<HeartbeatController> logger;

    public HeartbeatController(ILogger<HeartbeatController> logger)
    {
        this.logger = logger;
    }

    [Route(RoutePaths.Heartbeat, Name=RouteNames.Heartbeat)] 
    public ContentResult Index()
    {
        this.logger.LogTrace("Heartbeat called");
        return this.Content(Program.Version);
    }
}
