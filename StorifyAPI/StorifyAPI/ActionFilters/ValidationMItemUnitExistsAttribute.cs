using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StorifyAPI.ActionFilters
{
    public class ValidationMItemUnitExistsAttribute : IAsyncActionFilter
    {

        private readonly ILoggerManager _Logger;
        private readonly IRepositoryManager _repository;
        public ValidationMItemUnitExistsAttribute(ILoggerManager logger, IRepositoryManager repository) 
        {
            _Logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");

            var unitId = (Guid)context.ActionArguments["unitId"];
            var itemId = (Guid)context.ActionArguments["itemId"];

            var itemUnit = await _repository.MItemUnit.GetItemUnitByUnitAsync(itemId, unitId, trackChanges);

            if(itemUnit == null) 
            {
                _Logger.LogError($"No Material Item Unit With Item Id : {itemId}, and Unit If : {unitId} Exist In The Database");
                context.Result = new NotFoundObjectResult("No Material Item Unit Match The Request");
            }
            else
            {
                context.HttpContext.Items.Add("mItemUnit", itemUnit);
                await next();
            }
            
        }
    }
}
