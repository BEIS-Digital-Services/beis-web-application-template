
namespace Beis.WebApplication.Controllers
{
    public class AccessibilityStatementController : Controller
    {
        public AccessibilityStatementController() 
        {
            
        }

        [HttpGet(RoutePaths.AccessibilityStatementPage, Name = RouteNames.AccessibilityStatementPage)]
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
