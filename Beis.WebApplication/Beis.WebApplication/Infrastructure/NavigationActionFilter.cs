using Beis.WebApplication.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.ObjectModel;

namespace Beis.WebApplication.Infrastructure
{
    public class NavigationActionFilter : IActionFilter
    {
        private readonly ISessionService _sessionService;

        public NavigationActionFilter()
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        Collection<string> controllersToIgnore = new Collection<string>
        {
            nameof(HomeController)
        };

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var modelState = context.ModelState;
            if (!modelState.IsValid && context.Controller is Controller controller)
            {
                if (controllersToIgnore.Contains(nameof(controller)))
                    return;
                context.Result = new ViewResult
                {
                    ViewName = context.RouteData.Values["Action"].ToString()

                };
            }

        }
    }




}
