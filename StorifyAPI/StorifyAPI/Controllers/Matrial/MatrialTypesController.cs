using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorifyAPI.Context;
using Microsoft.EntityFrameworkCore;
using StorifyAPI.Models.Matrial;

namespace StorifyAPI.Controllers.Matrial
{
    [ApiController]
    [Route("api/MatrialTypes")]

    public class MatrialTypesController : ControllerBase
    {
        private StorifyContext _context { get; }

        public MatrialTypesController(StorifyContext context)
        {
            _context = context;
        }

        // GET -> api/MatrialTypes
        [HttpGet("")]
        public async Task<IActionResult> GetTypes()
        {
            try
            {
                return Ok(await _context.matrialTypes.ToListAsync());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        // GET -> api/MatrialTypes/{id}
        [HttpGet("{id}")]
        public IActionResult GetTypeByID(int id)
        {
            try
            {
                return Ok(_context.matrialTypes.Find(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Post -> api/MatrialTypes
        [HttpPost("")]
        public IActionResult AddType([FromBody] MatrialType MType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _context.matrialTypes.Add(MType);
                _context.SaveChanges();
                return CreatedAtRoute("", new { id = MType.ID }, MType);
            }
            catch (Exception ex)
            {
                if (isCodeExist(MType.Code))
                    return Conflict("The Code Already Exist");
                return BadRequest(ex.Message);
            }

        }

        // Put -> api/MatrialTypes/{id}
        [HttpPut("{id}")]
        public IActionResult EditType(int id, [FromBody] MatrialType MType)
        {
            if (!isIDExist(id))
                return NotFound("id Not Found");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Entry(MType).State = EntityState.Modified;
                _context.SaveChanges();
                return CreatedAtRoute("", new { id = MType.ID }, MType);
            }
            catch (Exception ex)
            {
                if (isCodeExist(MType.Code))
                    return Conflict("The Code Already Exist");
                return BadRequest(ex.Message);
            }

        }

        // Delete -> api/MatrialTypes/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTypeByID(int id)
        {
            var type = _context.matrialTypes.Find(id);

            if (type == null)
                return NotFound("No Type Match The ID");
            try
            {
                _context.matrialTypes.Remove(type);
                _context.SaveChanges();
                return Ok("Type Removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private bool isCodeExist(string Code)
        {
            return _context.matrialTypes.Count(Type => Type.Code == Code) > 0;
        }
        private bool isIDExist(int id)
        {
            return _context.matrialTypes.Count(Type => Type.ID == id) > 0;
        }
    }
}
