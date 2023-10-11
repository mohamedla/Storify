using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorifyAPI.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Contracts;
using StorifyAPI.ActionFilters;
using Entities.DataTransferObjects.Material;
using Entities.Models.Material;

namespace StorifyAPI.Controllers.Material
{
    [ApiController]
    [Route("api/MaterialItems/{itemId}/MaterialItemUnits")]

    public class MaterialItemUnitsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public MaterialItemUnitsController(ILoggerManager logger, IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repository = repositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

        // GET -> api/MaterialItemUnits
        [HttpGet("", Name = nameof(GetItemUnits))]
        [ServiceFilter(typeof(ValidationMItemExistsAttribute))]
        public async Task<IActionResult> GetItemUnits(Guid itemId)
        {
            var itemUnits = await _repository.MItemUnit.GetAllUnitForItemAsync(itemId, false);

            var itemUnitsDTO = _mapper.Map<IEnumerable<MaterialItemUnitDTO>>(itemUnits);

            return Ok(itemUnitsDTO);
        }

        // GET -> api/MaterialItemUnits/{id}
        [HttpGet("{unitId}")]
        [ServiceFilter(typeof(ValidationMItemExistsAttribute))]
        [ServiceFilter(typeof(ValidationMItemUnitExistsAttribute))]
        public async Task<IActionResult> GetItemUnitByUnit(Guid itemId, Guid unitId)
        {
            var itemUnit = HttpContext.Items["mItemUnit"] as MaterialItemUnit;

            var itemUnitDTO = _mapper.Map<MaterialItemUnitDTO>(itemUnit);

            return Ok(itemUnitDTO);
        }

        // Post -> api/MaterialItemUnits
        [HttpPost("")]
        [ServiceFilter(typeof(ValidationMItemExistsAttribute))]
        [ServiceFilter(typeof(ValidationMUnitExistsAttribute))]
        public async Task<IActionResult> AddItemUnit(Guid itemId, [FromBody] MaterialItemUnitCreateDTO itemUnitDTO)
        {
            var itemUnitValidation = await _repository.MItemUnit.GetItemUnitByUnitAsync(itemId, itemUnitDTO.UnitId, false);

            if(itemUnitValidation != null) 
                return BadRequest("The Item already has this Unit");

            var itemUnits = await _repository.MItemUnit.GetAllUnitForItemAsync(itemId, false);

            var itemUnit = _mapper.Map<MaterialItemUnit>(itemUnitDTO);

            // Frist inserted unit is the main and smallest unit and other unit be Greater 
            if (itemUnits.Count() == 0)
            {
                itemUnit.IsMain = true;
                itemUnit.CFactor = 1;
            }

            _repository.MItemUnit.CreateItemUnit(itemId, itemUnit);
            await _repository.SaveAsync();

            itemUnits = await _repository.MItemUnit.GetAllUnitForItemAsync(itemId, false);

            var itemUnitsDTO = _mapper.Map<IEnumerable<MaterialItemUnitDTO>>(itemUnits);

            return CreatedAtRoute(nameof(GetItemUnits), new { itemId = itemId }, itemUnitsDTO);
        }

        // Put -> api/MaterialItemUnits/{id}
        [HttpPut("{unitId}")]
        [ServiceFilter(typeof(ValidationMItemUnitExistsAttribute))]
        public async Task<IActionResult> EditItemUnitPrice(Guid itemId, Guid unitId, [FromBody] MaterialItemUnitUpdateDTO itemUnitDTO)
        {
            var itemUnit = HttpContext.Items["mItemUnit"] as MaterialItemUnit;

            if(!itemUnit.IsMain)
                return BadRequest("Can't Update Unit Price For Non Main Unit");

            _mapper.Map(itemUnitDTO, itemUnit);
            await _repository.SaveAsync();

            _repository.MItemUnit.ChangeMainItemUnitPriceAsync(itemId, itemUnit.UnitId);

            return NoContent();

        }

        // Delete -> api/MaterialItemUnits/{id}
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationMItemUnitExistsAttribute))]
        public async Task<IActionResult> DeleteTypeByID(Guid itemId, Guid unitId)
        {
            var itemUnit = HttpContext.Items["mItemUnit"] as MaterialItemUnit;

            _repository.MItemUnit.DeleteItemUnit(itemUnit);
            await _repository.SaveAsync();

            return NoContent();
        }


    }
}
