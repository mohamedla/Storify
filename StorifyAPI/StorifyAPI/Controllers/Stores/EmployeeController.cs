using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
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

        [HttpGet("{Id}", Name = "GetEmployeeForStore")]
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

        [HttpPost("")]
        public IActionResult GetEmployeeForStore(Guid StoreId, [FromBody] EmployeeCreateDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                _logger.LogError("Employee Create DTO Object Sent from client is null");
                return BadRequest("Employee Is Empty");
            }

            var store = _repositoryManager.Store.GetStore(StoreId, false);

            if (store == null)
            {
                _logger.LogInfo($"No Store With Id : {StoreId} Exist In The Database");
                return NotFound();
            }

            var employee = _mapper.Map<Employee>(employeeDTO);

            _repositoryManager.Employee.CreateEmployeeForStore(StoreId, employee);
            _repositoryManager.Save();

            var returnEmployee = _mapper.Map<EmployeeDTO>(employee);

            return CreatedAtRoute("GetEmployeeForStore", new { StoreId, Id = returnEmployee.Id }, returnEmployee);
        }
    }
}
