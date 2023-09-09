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
    [Route("api/MatrialGroups")]

    public class MatrialGroupsController : ControllerBase
    {

        private MGroupRepository _groupRepository;

        public MatrialGroupsController(StorifyContext context)
        {
            _groupRepository = new MGroupRepository(context);
        }

        // GET -> api/MatrialTypes
        [HttpGet("")]
        public async Task<IActionResult> GetGroups()
        {
            try
            {
                var groups = from g in await _groupRepository.GetAllAsync()
                             select new
                             {
                                 g.ID,
                                 g.Code,
                                 g.GlobalName,
                                 g.LocalName,
                                 matrialType = new
                                 {
                                     g.matrialType.ID,
                                     g.matrialType.GlobalName,
                                     g.matrialType.LocalName
                                 }
                             };

                //return Ok(await _context.matrialGroups.Include("matrialType").ToListAsync());
                return Ok(groups.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        // GET -> api/MatrialTypes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupByID(int id)
        {
            try
            {
                var group = await _groupRepository.GetByIdAsync(id);
                if (group == null) 
                    return NotFound();
                return Ok(group);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Post -> api/MatrialTypes
        [HttpPost("")]
        public IActionResult AddGroup([FromBody] MatrialGroup MGroup)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _groupRepository.AddAsync(ref MGroup);
                return CreatedAtRoute("", new { id = MGroup.ID }, MGroup);
            }
            catch (Exception ex)
            {
                if (_groupRepository.isCodeExist(MGroup.Code))
                    return Conflict("The Code Already Exist");
                return BadRequest(ex);
            }

        }

        // Put -> api/MatrialTypes/{id}
        [HttpPut("{id}")]
        public IActionResult EditGroup(int id, [FromBody] MatrialGroup MGroup)
        {
            if (id != MGroup.ID)
                return BadRequest("URL ID Confict With Body ID");

            if (!_groupRepository.isIDExist(id))
                return NotFound("id Not Found");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _groupRepository.UpdateAsync(MGroup);
                return CreatedAtRoute("", new { id = MGroup.ID }, MGroup);
            }
            catch (Exception ex)
            {
                if (_groupRepository.isCodeExist(MGroup.Code))
                    return Conflict("The Code Already Exist");
                return BadRequest(ex.Message);
            }

        }

        // Delete -> api/MatrialTypes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupByID(int id)
        {
            var group = await _groupRepository.GetByIdAsync(id);

            if (group == null)
                return NotFound("No Type Match The ID");
            try
            {
                await _groupRepository.DeleteAsync(group);
                return Ok("Group Removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
