using AutoMapper;
using Azure;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<IActionResult> GetEmployeeForStoreAsync(Guid StoreId)
        {
            var store = await _repositoryManager.Store.GetStoreAsync(StoreId, false);

            if (store == null)
            {
                _logger.LogInfo($"No Store With Id : {StoreId} Exist In The Database");
                return NotFound();
            }

            var employees = await _repositoryManager.Employee.GetEmployeesAsync(StoreId, false);
            var employeesDTO = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

            return Ok(employeesDTO);

        }

        [HttpGet("{Id}", Name = "GetEmployeeForStore")]
        public async Task<IActionResult> GetEmployeeForStoreAsync(Guid StoreId, Guid Id)
        {
            var store = await _repositoryManager.Store.GetStoreAsync(StoreId, false);

            if (store == null)
            {
                _logger.LogInfo($"No Store With Id : {StoreId} Exist In The Database");
                return NotFound();
            }

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
        public async Task<IActionResult> CreateEmployeeForStoreAsync(Guid StoreId, [FromBody] EmployeeCreateDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                _logger.LogError("Employee Create DTO Object Sent from client is null");
                return BadRequest("Employee Is Empty");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model State For The EmployeeCreateDTO Object");
                return UnprocessableEntity(ModelState);
            }

            var store = await _repositoryManager.Store.GetStoreAsync(StoreId, false);

            if (store == null)
            {
                _logger.LogInfo($"No Store With Id : {StoreId} Exist In The Database");
                return NotFound();
            }

            var employee = _mapper.Map<Employee>(employeeDTO);

            _repositoryManager.Employee.CreateEmployeeForStore(StoreId, employee);
            await _repositoryManager.SaveAsync();

            var returnEmployee = _mapper.Map<EmployeeDTO>(employee);

            return CreatedAtRoute("GetEmployeeForStore", new { StoreId, Id = returnEmployee.Id }, returnEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid StoreId, Guid id)
        {
            var store = await _repositoryManager.Store.GetStoreAsync(StoreId, false);

            if (store == null)
            {
                _logger.LogInfo($"No Store With Id : {StoreId} Exist In The Database");
                return NotFound("No Store Exist With This ID");
            }

            var employee = await _repositoryManager.Employee.GetEmployeeAsync(StoreId, id, false);
            if (employee == null)
            {
                _logger.LogError($"No Employee with id: {id} found in DB");
                return NotFound("No Employee Exist With This ID");
            }

            _repositoryManager.Employee.DeleteEmployee(employee);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeForStoreAsync(Guid StoreId, Guid id, [FromBody] EmployeeUpdateDTO employeeDTO)
        {
            if (employeeDTO == null)
            {
                _logger.LogError("Employee Create DTO Object Sent from client is null");
                return BadRequest("Employee Is Empty");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid Model State For The EmployeeUpdateDTO Object");
                return UnprocessableEntity(ModelState);
            }

            var store = await _repositoryManager.Store.GetStoreAsync(StoreId, false);

            if (store == null)
            {
                _logger.LogInfo($"No Store With Id : {StoreId} Exist In The Database");
                return NotFound();
            }

            var employee = await _repositoryManager.Employee.GetEmployeeAsync(StoreId, id, true);
            if (store == null)
            {
                _logger.LogInfo($"No Employee With Id : {id} Exist In The Database");
                return NotFound();
            }

            _mapper.Map(employeeDTO, employee);
            await _repositoryManager.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForStoreAsync(Guid StoreId, Guid id, [FromBody] JsonPatchDocument<EmployeeUpdateDTO> patchEmployeeDTO)
        {
            if (patchEmployeeDTO == null)
            {
                _logger.LogError("Employee Create DTO Object Sent from client is null");
                return BadRequest("Employee Is Empty"); 
            }

            var store = await _repositoryManager.Store.GetStoreAsync(StoreId, false);

            if (store == null)
            {
                _logger.LogInfo($"No Store With Id : {StoreId} Exist In The Database");
                return NotFound();
            }

            var employee = await _repositoryManager.Employee.GetEmployeeAsync(StoreId, id, true);
            if (store == null)
            {
                _logger.LogInfo($"No Employee With Id : {id} Exist In The Database");
                return NotFound();
            }

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
