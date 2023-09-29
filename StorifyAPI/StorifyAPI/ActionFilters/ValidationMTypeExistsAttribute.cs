using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StorifyAPI.ActionFilters
{
    public class ValidationMTypeExistsAttribute : IAsyncActionFilter
    {

        private readonly ILoggerManager _Logger;
        private readonly IRepositoryManager _repository;
        public ValidationMTypeExistsAttribute(ILoggerManager logger, IRepositoryManager repository) 
        {
            _Logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");

            var id = (Guid)context.ActionArguments["id"];

            var store = await _repository.MType.GetTypeAsync(id, trackChanges);

            if(store == null) 
            {
                _Logger.LogError($"No Material Type With Id : {id} Exist In The Database");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("mType", store);
                await next();
            }
            
        }
    }
}
