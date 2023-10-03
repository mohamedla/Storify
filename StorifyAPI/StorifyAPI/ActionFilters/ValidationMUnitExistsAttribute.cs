using Contracts;
using Entities.DataTransferObjects.Material;
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

            var idTmp = context.ActionArguments.SingleOrDefault(x => x.Key.ToString().Equals("id")).Value;

            var id = idTmp == null ? new Guid("00000000-0000-0000-0000-000000000000") : (Guid)idTmp;

            var itemUnit = context.ActionArguments.SingleOrDefault(x => x.Key.ToString().Contains("itemUnitDTO")).Value as MaterialItemUnitManipulationDTO;

            var unit = itemUnit == null ? await _repository.MUnit.GetEntityAsync(id, trackChanges) : await _repository.MUnit.GetEntityAsync(itemUnit.UnitId, false);

            if(unit == null) 
            {
                _Logger.LogError($"No Material Unit With Id : {id} Exist In The Database");
                context.Result = new NotFoundObjectResult("No Material Unit Match The Request");
            }
            else
            {
                context.HttpContext.Items.Add("mUnit", unit);
                await next();
            }
            
        }
    }
}
