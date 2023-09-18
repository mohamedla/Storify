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
    [Route("api/MatrialItemUnits")]

    public class MatrialItemUnitsController : ControllerBase
    {
        private MItemUnitRepository _itemUnitRepository;

        public MatrialItemUnitsController (StorifyContext context)
        {
            _itemUnitRepository = new MItemUnitRepository(context);
        }

        // GET -> api/MatrialItemUnits
        [HttpGet("")]
        public async Task<IActionResult> GetTypes()
        {
            try
            {
                return Ok(await _itemUnitRepository.GetAllAsync());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        // GET -> api/MatrialItemUnits/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeByID(int id)
        {
            try
            {
                var type = await _itemUnitRepository.GetByIdAsync(id);
                if(type == null)
                    return NotFound();
                return Ok(type);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Post -> api/MatrialItemUnits
        [HttpPost("")]
        public async Task<IActionResult> AddType([FromBody] MatrialItemUnit MItemUnit)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _itemUnitRepository.AddAsync(ref MItemUnit);
                return CreatedAtRoute("", new { id = MItemUnit.ID }, MItemUnit);
            }
            catch (Exception ex)
            {
                if (_itemUnitRepository.isItemUnitExist(MItemUnit.ItemID, MItemUnit.UnitID))
                    return Conflict("The Unit Already Exist");
                return BadRequest(ex.Message);
            }

        }

        // Put -> api/MatrialItemUnits/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditType(int id, [FromBody] MatrialItemUnit MItemUnit)
        {
            //var matrialItemUnitFromDb = await _itemUnitRepository.GetByIdAsync(id);
            if (id != MItemUnit.ID) 
                return BadRequest("ID Conflict");
            if (_itemUnitRepository.isIDExist(id)) 
                return NotFound("ID Not Found");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _itemUnitRepository.UpdateAsync(MItemUnit);
                return CreatedAtRoute("", new { id = MItemUnit.ID }, MItemUnit);
            }
            catch (Exception ex)
            {
                if (_itemUnitRepository.isItemUnitExist(MItemUnit.ItemID, MItemUnit.UnitID))
                    return Conflict("The Unit Already Exist");
                return BadRequest(ex.Message);
            }

        }

        // Delete -> api/MatrialItemUnits/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeByID(int id)
        {
            var type = await _itemUnitRepository.GetByIdAsync(id);

            if (type == null)
                return NotFound("No Item With This Unit Match The ID");
            try
            {
                await _itemUnitRepository.DeleteAsync(type);
                return Ok("Unit Removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
