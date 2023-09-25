using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace StorifyAPI.Controllers.Stores
{
    [Route("api/Stores/{StoreId}/Employees")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeController(ILoggerManager logger, IRepositoryManager repositoryManager, IMapper mapper)
        {

            _logger = logger;
            _repositoryManager = repositoryManager;
            _mapper = mapper;

        }

        [HttpGet("")]
        public IActionResult GetEmployeeForStore(Guid StoreId)
        {
            var store = _repositoryManager.Store.GetStore(StoreId, false);

            if (store == null)
            {
                _logger.LogInfo($"No Store With Id : {StoreId} Exist In The Database");
                return NotFound();
            }

            var employees = _repositoryManager.Employee.GetEmployees(StoreId, false);
            var employeesDTO = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            return Ok(employeesDTO);

        }

        [HttpGet("{Id}")]
        public IActionResult GetEmployeeForStore(Guid StoreId, Guid Id)
        {
            var store = _repositoryManager.Store.GetStore(StoreId, false);

            if (store == null)
            {
                _logger.LogInfo($"No Store With Id : {StoreId} Exist In The Database");
                return NotFound();
            }

            var employee = _repositoryManager.Employee.GetEmployee(StoreId, Id, false);
            if (employee == null)
            {
                _logger.LogInfo($"No Employee With Id : {Id} Exist In The Database");
                return NotFound();
            }
            else
            {
                var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
                return Ok(employeeDTO);
            }

        }
    }
}
