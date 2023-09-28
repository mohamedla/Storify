using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StorifyAPI.ActionFilters
{
    public class ValidationStoreExistsAttribute : IAsyncActionFilter
    {

        private readonly ILoggerManager _Logger;
        private readonly IRepositoryManager _repository;
        public ValidationStoreExistsAttribute(ILoggerManager logger, IRepositoryManager repository) 
        {
            _Logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");

            var id = context.HttpContext.Request.Method.Equals("GET") ? (Guid)context.ActionArguments["StoreId"] : (Guid)context.ActionArguments["id"];

            var store = await _repository.Store.GetStoreAsync(id, trackChanges);

            if(store == null) 
            {
                _Logger.LogError($"No Store With Id : {id} Exist In The Database");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("store", store);
                await next();
            }
            
        }
    }
}
