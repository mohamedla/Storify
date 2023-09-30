using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StorifyAPI.ActionFilters
{
    public class ValidationMUnitExistsAttribute : IAsyncActionFilter
    {

        private readonly ILoggerManager _Logger;
        private readonly IRepositoryManager _repository;
        public ValidationMUnitExistsAttribute(ILoggerManager logger, IRepositoryManager repository) 
        {
            _Logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");

            var id = (Guid)context.ActionArguments["id"];

            var item = await _repository.MUnit.GetEntityAsync(id, trackChanges);

            if(item == null) 
            {
                _Logger.LogError($"No Material Unit With Id : {id} Exist In The Database");
                context.Result = new NotFoundObjectResult("No Material Unit Match The Request");
            }
            else
            {
                context.HttpContext.Items.Add("mUnit", item);
                await next();
            }
            
        }
    }
}
