
using Beis.WebApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Beis.WebApplication.Infrastructure
{
    public class SessionRequiredActionFilter : IActionFilter
    {
        private readonly ISessionService _sessionService;

        public SessionRequiredActionFilter(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is HomeController 
                or SessionExpiredController 
              
     
                )
            {
                return;
            }

            if (!_sessionService.HasValidSession(context.HttpContext) && context.Controller is Controller controller)
            {
                context.Result = controller.RedirectToRoute(RouteNames.SessionExpiredPage, RoutePaths.SessionExpiredPage);            
            }
        }
    }
}