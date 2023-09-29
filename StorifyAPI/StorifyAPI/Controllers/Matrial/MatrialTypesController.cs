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
using Contracts;
using AutoMapper;
using Entities.Models.Material;
using StorifyAPI.ActionFilters;
using Entities.DataTransferObjects.Material;

namespace StorifyAPI.Controllers.Material
{
    [ApiController]
    [Route("api/MaterialTypes")]

    public class MaterialTypesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public MaterialTypesController(ILoggerManager logger, IRepositoryManager repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        // GET -> api/MaterialTypes
        [HttpGet("")]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _repository.MType.GetAllTypesAsync(false);

            var typesDTO = _mapper.Map<IEnumerable<MaterialType>>(types);

            return Ok(typesDTO);
        }

        // GET -> api/MaterialTypes/{id}
        [HttpGet("{id}", Name = nameof(GetTypeByID))]
        [ServiceFilter(typeof(ValidationMTypeExistsAttribute))]
        public async Task<IActionResult> GetTypeByID(Guid id)
        {
            var type = HttpContext.Items["mType"] as MaterialType ;

            var typesDTO = _mapper.Map<MaterialTypeDTO>(type);

            return Ok(typesDTO);
        }

        // Post -> api/MaterialTypes
        [HttpPost("")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> AddType([FromBody] MaterialTypeCreateDTO typeDTO)
        {
            var type = _mapper.Map<MaterialType>(typeDTO);

            _repository.MType.CreateType(type);
            await _repository.SaveAsync();

            var returnType = _mapper.Map<MaterialTypeDTO>(type);

            return CreatedAtRoute(nameof(GetTypeByID), new { id = returnType.Id }, returnType);

        }

        // Put -> api/MaterialTypes/{id}
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationMTypeExistsAttribute))]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> EditType(Guid id, [FromBody] MaterialTypeUpdateDTO typeDTO)
        {
            var type = HttpContext.Items["mType"] as MaterialType;

            _mapper.Map(typeDTO, type);
            await _repository.SaveAsync();

            return NoContent();
        }

        // Delete -> api/MaterialTypes/{id}
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationMTypeExistsAttribute))]
        public async Task<IActionResult> DeleteTypeByID(Guid id)
        {
            var type = HttpContext.Items["mType"] as MaterialType;

            _repository.MType.DeleteType(type);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
