using AutoMapper;
using Azure;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using StorifyAPI.ActionFilters;

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
        [ServiceFilter(typeof(ValidationStoreExistsAttribute))]
        public async Task<IActionResult> GetEmployeeForStoreAsync(Guid StoreId)
        {
            var store = HttpContext.Items["store"] as Store;

            var employees = await _repositoryManager.Employee.GetEmployeesAsync(StoreId, false);
            var employeesDTO = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            return Ok(employeesDTO);

        }

        [HttpGet("{Id}", Name = "GetEmployeeForStore")]
        [ServiceFilter(typeof(ValidationStoreExistsAttribute))]
        public async Task<IActionResult> GetEmployeeForStoreAsync(Guid StoreId, Guid Id)
        {
            var store = HttpContext.Items["store"] as Store;

            var employee = await _repositoryManager.Employee.GetEmployeeAsync(StoreId, Id, false);
            if (employee == null)
            {
                _logger.LogInfo($"No Employee With Id : {Id} Exist In The Database");
                return NotFound();
            }

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
            return Ok(employeeDTO);

        }

        [HttpPost("")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationStoreExistsAttribute))]
        public async Task<IActionResult> CreateEmployeeForStoreAsync(Guid StoreId, [FromBody] EmployeeCreateDTO employeeDTO)
        {
            var store = HttpContext.Items["store"] as Store;

            var employee = _mapper.Map<Employee>(employeeDTO);

            _repositoryManager.Employee.CreateEmployeeForStore(StoreId, employee);
            await _repositoryManager.SaveAsync();

            var returnEmployee = _mapper.Map<EmployeeDTO>(employee);

            return CreatedAtRoute("GetEmployeeForStore", new { StoreId, Id = returnEmployee.Id }, returnEmployee);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationStoreEmployeeExistsAttribute))]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid StoreId, Guid id)
        {
            var employee = HttpContext.Items["employee"] as Employee;

            _repositoryManager.Employee.DeleteEmployee(employee);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidationStoreEmployeeExistsAttribute))]
        public async Task<IActionResult> UpdateEmployeeForStoreAsync(Guid StoreId, Guid id, [FromBody] EmployeeUpdateDTO employeeDTO)
        {
            var employee = HttpContext.Items["employee"] as Employee;

            _mapper.Map(employeeDTO, employee);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        [ServiceFilter(typeof(ValidationStoreEmployeeExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateEmployeeForStoreAsync(Guid StoreId, Guid id, [FromBody] JsonPatchDocument<EmployeeUpdateDTO> patchEmployeeDTO)
        {
            if (patchEmployeeDTO == null)
            {
                _logger.LogError("Employee Create DTO Object Sent from client is null");
                return BadRequest("Employee Is Empty"); 
            }

            var employee = HttpContext.Items["employee"] as Employee;

            var employeeDTO = _mapper.Map<EmployeeUpdateDTO>(employee);

            patchEmployeeDTO.ApplyTo(employeeDTO,ModelState);

            TryValidateModel(employeeDTO);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model State For The Employee JsonPatchDocument Object");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(employeeDTO, employee);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }
    }
}
