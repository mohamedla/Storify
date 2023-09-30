using Microsoft.AspNetCore.Mvc;
using StorifyAPI.Context;
using StorifyAPI.Repositories.MatrialRepo;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Material;
using StorifyAPI.ActionFilters;
using Entities.Models.Material;

namespace StorifyAPI.Controllers.Material
{
    [ApiController]
    [Route("api/MaterialGroups")]

    public class MaterialGroupsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public MaterialGroupsController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        // GET -> api/MaterialGroups
        [HttpGet("")]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _repository.MGroup.GetAllEntitiesAsync(false);

            var groupsDTO = _mapper.Map<IEnumerable<MaterialGroupDTO>>(groups);

            return Ok(groupsDTO);
        }

        // GET -> api/MaterialGroups/{id}
        [HttpGet("{id}", Name = nameof(GetGroupByID))]
        [ServiceFilter(typeof(ValidationMGroupExistsAttribute))]
        public async Task<IActionResult> GetGroupByID(Guid id)
        {
            var group = HttpContext.Items["mGroup"] as MaterialGroup;

            var groupDTO = _mapper.Map<MaterialGroupDTO>(group);

            return Ok(groupDTO);
        }

        // Post -> api/MaterialGroups
        [HttpPost("")]
        [ServiceFilter(typeof(ValidationMTypeExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddGroup([FromBody] MaterialGroupCreateDTO groupDTO)
        {

            var group = _mapper.Map<MaterialGroup>(groupDTO);

            _repository.MGroup.CreateEntity(group);
            await _repository.SaveAsync();

            var returnGroup = _mapper.Map<MaterialGroupDTO>(group);

            return CreatedAtRoute(nameof(GetGroupByID), new { id = returnGroup.Id }, returnGroup);

        }

        // Put -> api/MaterialGroups/{id}
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationMGroupExistsAttribute))]
        [ServiceFilter(typeof(ValidationMTypeExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> EditGroup(Guid id, [FromBody] MaterialGroupUpdateDTO groupDTO)
        {
            var group = HttpContext.Items["mGroup"] as MaterialGroup;

            _mapper.Map(groupDTO, group);
            await _repository.SaveAsync();

            return NoContent();

        }

        // Delete -> api/MaterialGroups/{id}
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationMGroupExistsAttribute))]
        public async Task<IActionResult> DeleteGroupByID(Guid id)
        {
            var group = HttpContext.Items["mGroup"] as MaterialGroup;

            _repository.MGroup.DeleteEntity(group);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
