using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StorifyAPI.ActionFilters
{
    public class ValidationMGroupExistsAttribute : IAsyncActionFilter
    {

        private readonly ILoggerManager _Logger;
        private readonly IRepositoryManager _repository;
        public ValidationMGroupExistsAttribute(ILoggerManager logger, IRepositoryManager repository) 
        {
            _Logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");

            var id = (Guid)context.ActionArguments["id"];

            var store = await _repository.MGroup.GetGroupAsync(id, trackChanges);

            if(store == null) 
            {
                _Logger.LogError($"No Material Group With Id : {id} Exist In The Database");
                context.Result = new NotFoundObjectResult("No Material Group Match The Rquest");
            }
            else
            {
                context.HttpContext.Items.Add("mGroup", store);
                await next();
            }
            
        }
    }
}
