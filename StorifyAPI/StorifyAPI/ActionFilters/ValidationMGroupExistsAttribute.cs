using Contracts;
using Entities.DataTransferObjects.Material;
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

            var idTmp = context.ActionArguments.SingleOrDefault(x => x.Key.ToString().Equals("id")).Value;

            var id = idTmp == null ? new Guid("00000000-0000-0000-0000-000000000000") : (Guid)idTmp;

            var item = context.ActionArguments.SingleOrDefault(x => x.Key.ToString().Contains("itemDTO")).Value as MaterialItemManipulationDTO;

            var group = item == null ? await _repository.MGroup.GetEntityAsync(id, trackChanges) : await _repository.MGroup.GetEntityAsync(item.MGroupId, false);


            if(group == null) 
            {
                _Logger.LogError($"No Material Group With Id : {id} Exist In The Database");
                context.Result = new NotFoundObjectResult("No Material Group Match The Request");
            }
            else
            {
                context.HttpContext.Items.Add("mGroup", group);
                await next();
            }
            
        }
    }
}
