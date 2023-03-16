using Microsoft.AspNetCore.Mvc;

namespace Beis.WebApplication.Controllers
{
    public class MyAccountController : Controller
    {
        [Route(RoutePaths.MyAccountPage, Name = RouteNames.MyAccountPage)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
