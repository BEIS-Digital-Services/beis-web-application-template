
using Microsoft.AspNetCore.Mvc;

namespace Beis.Ebss.ApplicationPortal.Web.Controllers
{
    public class CookiesController : Controller
    {
        public CookiesController() 
        {
            
        }

        [HttpGet(RoutePaths.CookiesPage, Name = RouteNames.CookiesPage)]
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
