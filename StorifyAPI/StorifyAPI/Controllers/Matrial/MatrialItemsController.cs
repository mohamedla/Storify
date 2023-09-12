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
    [Route("api/MatrialItems")]

    public class MatrialItemsController : ControllerBase
    {

        private MItemRepository _itemRepository;

        public MatrialItemsController(StorifyContext context)
        {
            _itemRepository = new MItemRepository(context);
        }

        // GET -> api/MatrialItems
        [HttpGet("")]
        public async Task<IActionResult> GetGroups()
        {
            try
            {
                var groups = from g in await _itemRepository.GetAllAsync()
                             select new
                             {
                                 g.ID,
                                 g.Code,
                                 g.GlobalName,
                                 g.LocalName,
                                 matrialType = new
                                 {
                                     g.matrialGroup.ID,
                                     g.matrialGroup.GlobalName,
                                     g.matrialGroup.LocalName
                                 }
                             };
                return Ok(groups.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        // GET -> api/MatrialItems/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupByID(int id)
        {
            try
            {
                var group = await _itemRepository.GetByIdAsync(id);
                if (group == null) 
                    return NotFound();
                return Ok(group);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Post -> api/MatrialItems
        [HttpPost("")]
        public IActionResult AddGroup([FromBody] MatrialItem MGroup)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _itemRepository.AddAsync(ref MGroup);
                return CreatedAtRoute("", new { id = MGroup.ID }, MGroup);
            }
            catch (Exception ex)
            {
                if (_itemRepository.isCodeExist(MGroup.Code))
                    return Conflict("The Code Already Exist");
                return BadRequest(ex);
            }

        }

        // Put -> api/MatrialItems/{id}
        [HttpPut("{id}")]
        public IActionResult EditGroup(int id, [FromBody] MatrialItem MGroup)
        {
            if (id != MGroup.ID)
                return BadRequest("URL ID Confict With Body ID");

            if (!_itemRepository.isIDExist(id))
                return NotFound("id Not Found");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _itemRepository.UpdateAsync(MGroup);
                return CreatedAtRoute("", new { id = MGroup.ID }, MGroup);
            }
            catch (Exception ex)
            {
                if (_itemRepository.isCodeExist(MGroup.Code))
                    return Conflict("The Code Already Exist");
                return BadRequest(ex.Message);
            }

        }

        // Delete -> api/MatrialItems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupByID(int id)
        {
            var group = await _itemRepository.GetByIdAsync(id);

            if (group == null)
                return NotFound("No Item Match The ID");
            try
            {
                await _itemRepository.DeleteAsync(group);
                return Ok("Item Removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
