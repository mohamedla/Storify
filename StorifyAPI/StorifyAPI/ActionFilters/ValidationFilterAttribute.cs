using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StorifyAPI.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {

        private readonly ILoggerManager _Logger;
        public ValidationFilterAttribute(ILoggerManager logger) 
        {
            _Logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context){ }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];

            var controller = context.RouteData.Values["controller"];

            var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("DTO")).Value;

            if(param == null) 
            {
                _Logger.LogError($"Object sent from client is null. Controller: {controller},action: {action}");
                context.Result = new BadRequestObjectResult($"Object is null. Controller:{ controller }, action: { action}");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                _Logger.LogError($"Invalid model state for the object. Controller:{controller}, action: {action}");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
