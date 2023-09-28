using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StorifyAPI.ActionFilters
{
    public class ValidationStoreEmployeeExistsAttribute : IAsyncActionFilter
    {

        private readonly ILoggerManager _Logger;
        private readonly IRepositoryManager _repository;
        public ValidationStoreEmployeeExistsAttribute(ILoggerManager logger, IRepositoryManager repository) 
        {
            _Logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;

            var trackChanges = method.Equals("PUT") || method.Equals("PATCH") ? true : false ;

            var storeId = (Guid)context.ActionArguments["StoreId"];

            var store = await _repository.Store.GetStoreAsync(storeId, false);

            if (store == null)
            {
                _Logger.LogError($"No Store With Id : {storeId} Exist In The Database");
                context.Result = new NotFoundResult();
            }

            var id = (Guid)context.ActionArguments["id"];

            var employee = await _repository.Employee.GetEmployeeAsync(storeId, id, trackChanges);

            if(employee == null) 
            {
                _Logger.LogError($"No Employee With Id : {id} Exist In The Database");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("employee", employee);
                await next();
            }
            
        }
    }
}
