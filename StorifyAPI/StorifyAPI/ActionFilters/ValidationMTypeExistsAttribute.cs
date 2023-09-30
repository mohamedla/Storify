using Contracts;
using Entities.DataTransferObjects.Material;
using Entities.Models.Material;
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

            var idTmp = context.ActionArguments.SingleOrDefault(x => x.Key.ToString().Equals("id")).Value;

            var id = idTmp == null ? new Guid("00000000-0000-0000-0000-000000000000") : (Guid)idTmp;

            var group = context.ActionArguments.SingleOrDefault(x => x.Key.ToString().Contains("groupDTO")).Value as MaterialGroupManipulationDTO;

            var type = group == null ? await _repository.MType.GetEntityAsync(id, trackChanges) : await _repository.MType.GetEntityAsync(group.MTypeId, false);

            if(type == null) 
            {
                _Logger.LogError($"No Material Type With Id : {id} Exist In The Database");
                context.Result = new NotFoundObjectResult("No Material Type Found That Match Your Request");
            }
            else
            {
                context.HttpContext.Items.Add("mType", type);
                await next();
            }
            
        }
    }
}
