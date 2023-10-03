using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StorifyAPI.ActionFilters
{
    public class ValidationMItemExistsAttribute : IAsyncActionFilter
    {

        private readonly ILoggerManager _Logger;
        private readonly IRepositoryManager _repository;
        public ValidationMItemExistsAttribute(ILoggerManager logger, IRepositoryManager repository) 
        {
            _Logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");

            var idTmp = context.ActionArguments.SingleOrDefault(x => x.Key.ToString().Equals("itemId")).Value;

            var id = idTmp != null ? (Guid)idTmp : (Guid)context.ActionArguments["id"]; ;

            var item = await _repository.MItem.GetEntityAsync(id, trackChanges);

            if(item == null) 
            {
                _Logger.LogError($"No Material Item With Id : {id} Exist In The Database");
                context.Result = new NotFoundObjectResult("No Material Item Match The Request");
            }
            else
            {
                context.HttpContext.Items.Add("mItem", item);
                await next();
            }
            
        }
    }
}
