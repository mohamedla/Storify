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
    [Route("api/MatrialUnits")]

    public class MatrialUnitsController : ControllerBase
    {
        private MUnitRepository _unitRepository;

        public MatrialUnitsController (StorifyContext context)
        {
            _unitRepository = new MUnitRepository(context);
        }

        // GET -> api/MatrialUnits
        [HttpGet("")]
        public async Task<IActionResult> GetTypes()
        {
            try
            {
                return Ok(await _unitRepository.GetAllAsync());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        // GET -> api/MatrialUnits/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeByID(int id)
        {
            try
            {
                var type = await _unitRepository.GetByIdAsync(id);
                if(type == null)
                    return NotFound();
                return Ok(type);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Post -> api/MatrialUnits
        [HttpPost("")]
        public async Task<IActionResult> AddType([FromBody] MatrialUnit MUnit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _unitRepository.AddAsync(ref MUnit);
                return CreatedAtRoute("", new { id = MUnit.ID }, MUnit);
            }
            catch (Exception ex)
            {
                if (_unitRepository.isCodeExist(MUnit.Code))
                    return Conflict("The Code Already Exist");
                return BadRequest(ex.Message);
            }

        }

        // Put -> api/MatrialUnits/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditType(int id, [FromBody] MatrialUnit MUnit)
        {
            if (!_unitRepository.isIDExist(id))
                return NotFound("id Not Found");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _unitRepository.UpdateAsync(MUnit);
                return CreatedAtRoute("", new { id = MUnit.ID }, MUnit);
            }
            catch (Exception ex)
            {
                if (_unitRepository.isCodeExist(MUnit.Code))
                    return Conflict("The Code Already Exist");
                return BadRequest(ex.Message);
            }

        }

        // Delete -> api/MatrialUnits/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeByID(int id)
        {
            var type = await _unitRepository.GetByIdAsync(id);

            if (type == null)
                return NotFound("No Unit Match The ID");
            try
            {
                await _unitRepository.DeleteAsync(type);
                return Ok("Unit Removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
