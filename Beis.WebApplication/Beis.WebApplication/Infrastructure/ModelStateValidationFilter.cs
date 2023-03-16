using Microsoft.AspNetCore.Mvc.Filters;

namespace Beis.WebApplication.Infrastructure
{
    public class ModelStateValidationFilter : IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

            var modelState = context.ModelState;
            if (!modelState.IsValid && context.Controller is Controller controller)
            {

                context.Result = new ViewResult
                {
                    ViewName = context.RouteData.Values["Action"].ToString() //"Index",
                    // ViewData = context.RouteData
                };
            }

        }

    }
}
