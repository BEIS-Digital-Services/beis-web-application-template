using Microsoft.AspNetCore.Mvc;

namespace Beis.Ebss.ApplicationPortal.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return View("BadRequest");
                case 404:
                    return View("NotFound");
                case 500:
                    return View("InternalServer");
                default:
                    break;
            }
            return View("Error");
        }
    }
}
