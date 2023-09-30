using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorifyAPI.Context;
using Microsoft.EntityFrameworkCore;
using StorifyAPI.Models.Matrial;
using StorifyAPI.Repositories.MatrialRepo;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Material;
using StorifyAPI.ActionFilters;
using Entities.Models.Material;

namespace StorifyAPI.Controllers.Material
{
    [ApiController]
    [Route("api/MaterialUnits")]

    public class MaterialUnitsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public MaterialUnitsController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        // GET -> api/MaterialUnits
        [HttpGet("")]
        public async Task<IActionResult> GetTypes()
        {
            var units = await _repository.MUnit.GetAllEntitiesAsync(false);

            var unitsDTO = _mapper.Map<IEnumerable<MaterialUnitDTO>>(units);

            return Ok(unitsDTO);

        }

        // GET -> api/MaterialUnits/{id}
        [HttpGet("{id}", Name = nameof(GetUnitByID))]
        [ServiceFilter(typeof(ValidationMUnitExistsAttribute))]
        public async Task<IActionResult> GetUnitByID(Guid id)
        {
            var unit = HttpContext.Items["mUnit"] as MaterialUnit;

            var unitDTO = _mapper.Map<MaterialUnitDTO>(unit);

            return Ok(unitDTO);
        }

        // Post -> api/MaterialUnits
        [HttpPost("")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddType([FromBody] MaterialUnitCreateDTO unitDTO)
        {
            var unit = _mapper.Map<MaterialUnit>(unitDTO);

            _repository.MUnit.CreateEntity(unit);
            await _repository.SaveAsync();

            var returnItem = _mapper.Map<MaterialUnitDTO>(unit);

            return CreatedAtRoute(nameof(GetUnitByID), new { id = returnItem.Id }, returnItem);

        }

        // Put -> api/MaterialUnits/{id}
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationMUnitExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> EditType(Guid id, [FromBody] MaterialUnitUpdateDTO unitDTO)
        {
            var unit = HttpContext.Items["mUnit"] as MaterialUnit;

            _mapper.Map(unitDTO, unit);
            await _repository.SaveAsync();

            return NoContent();

        }

        // Delete -> api/MaterialUnits/{id}
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationMUnitExistsAttribute))]
        public async Task<IActionResult> DeleteTypeByID(Guid id)
        {
            var unit = HttpContext.Items["mUnit"] as MaterialUnit;

            _repository.MUnit.DeleteEntity(unit);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
