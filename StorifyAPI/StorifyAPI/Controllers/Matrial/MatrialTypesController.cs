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

namespace StorifyAPI.Controllers.Matrial
{
    [ApiController]
    [Route("api/MatrialTypes")]

    public class MatrialTypesController : ControllerBase
    {
        private MTypeRepository _TypeRepository;

        public MatrialTypesController(StorifyContext context)
        {
            _TypeRepository = new MTypeRepository(context);
        }

        // GET -> api/MatrialTypes
        [HttpGet("")]
        public async Task<IActionResult> GetTypes()
        {
            try
            {
                return Ok(await _TypeRepository.GetAllAsync());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        // GET -> api/MatrialTypes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeByID(int id)
        {
            try
            {
                var type = await _TypeRepository.GetByIdAsync(id);
                if(type == null)
                    return NotFound();
                return Ok(type);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Post -> api/MatrialTypes
        [HttpPost("")]
        public async Task<IActionResult> AddType([FromBody] MatrialType MType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _TypeRepository.AddAsync(ref MType);
                return CreatedAtRoute("", new { id = MType.ID }, MType);
            }
            catch (Exception ex)
            {
                if (_TypeRepository.isCodeExist(MType.Code))
                    return Conflict("The Code Already Exist");
                return BadRequest(ex.Message);
            }

        }

        // Put -> api/MatrialTypes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditType(int id, [FromBody] MatrialType MType)
        {
            if (!_TypeRepository.isIDExist(id))
                return NotFound("id Not Found");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _TypeRepository.UpdateAsync(MType);
                return CreatedAtRoute("", new { id = MType.ID }, MType);
            }
            catch (Exception ex)
            {
                if (_TypeRepository.isCodeExist(MType.Code))
                    return Conflict("The Code Already Exist");
                return BadRequest(ex.Message);
            }

        }

        // Delete -> api/MatrialTypes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeByID(int id)
        {
            var type = await _TypeRepository.GetByIdAsync(id);

            if (type == null)
                return NotFound("No Type Match The ID");
            try
            {
                await _TypeRepository.DeleteAsync(type);
                return Ok("Type Removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
