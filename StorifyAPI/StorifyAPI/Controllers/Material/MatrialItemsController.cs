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
using Entities.DataTransferObjects.Material;
using StorifyAPI.ActionFilters;
using Entities.Models.Material;

namespace StorifyAPI.Controllers.Material
{
    [ApiController]
    [Route("api/MaterialItems")]

    public class MaterialItemsController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public MaterialItemsController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        // GET -> api/MaterialItems
        [HttpGet("")]
        public async Task<IActionResult> GetItems()
        {
            var items = await _repository.MItem.GetAllEntitiesAsync(false);

            var itemsDTO = _mapper.Map<IEnumerable<MaterialItemDTO>>(items);

            return Ok(itemsDTO);

        }

        // GET -> api/MaterialItems/{id}
        [HttpGet("{id}", Name = nameof(GetItemByID))]
        [ServiceFilter(typeof(ValidationMItemExistsAttribute))]
        public async Task<IActionResult> GetItemByID(Guid id)
        {
            var item = HttpContext.Items["mItem"] as MaterialItem;

            var itemDTO = _mapper.Map<MaterialItemDTO>(item);

            return Ok(itemDTO);
        }

        // Post -> api/MaterialItems
        [HttpPost("")]
        [ServiceFilter(typeof(ValidationMGroupExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddGroup([FromBody] MaterialItemCreateDTO itemDTO)
        {
            var item = _mapper.Map<MaterialItem>(itemDTO);

            _repository.MItem.CreateEntity(item);
            await _repository.SaveAsync();

            var returnItem = _mapper.Map<MaterialItemDTO>(item);

            return CreatedAtRoute(nameof(GetItemByID), new { id = returnItem.Id }, returnItem);

        }

        // Put -> api/MaterialItems/{id}
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationMItemExistsAttribute))]
        [ServiceFilter(typeof(ValidationMGroupExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> EditGroup(Guid id, [FromBody] MaterialItemUpdateDTO itemDTO)
        {
            var item = HttpContext.Items["mItem"] as MaterialItem;

            _mapper.Map(itemDTO, item);
            await _repository.SaveAsync();

            return NoContent();

        }

        // Delete -> api/MaterialItems/{id}
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationMItemExistsAttribute))]
        public async Task<IActionResult> DeleteGroupByID(Guid id)
        {
            var item = HttpContext.Items["mItem"] as MaterialItem;

            _repository.MItem.DeleteEntity(item);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
