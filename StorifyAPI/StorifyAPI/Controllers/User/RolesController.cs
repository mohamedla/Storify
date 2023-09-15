using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.Employee;
using StorifyAPI.Models.User.FormModels;
using System.Data;

namespace StorifyAPI.Controllers.User
{
    [ApiController]
    [Route("api/Roles")]
    //[Authorize(Roles = RoleNames.admin)]
    public class RolesController : Controller
    {
        private readonly IdentityContext _context;

        public RolesController(IdentityContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Index()
        {
            return Ok(await  (from r in _context.Roles select new { r.Id , r.Name}).ToListAsync());
        }

        [HttpPost("")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleForm role)
        {
            if (!ModelState.IsValid)
                return BadRequest("Role Name Not Valid");
            try
            {
                var roleChk = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == role.Name.ToUpper());
                if ( roleChk is not null)
                    return BadRequest("The Name Already Exist");

                await _context.Roles.AddAsync(new IdentityRole { Name = role.Name.Trim(), NormalizedName = role.Name.Trim().ToUpper() });
                _context.SaveChanges();
                return Ok(await _context.Roles.ToListAsync());

            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(RoleForm role)
        {
            if (!ModelState.IsValid)
                return BadRequest("Role Name Not Valid");

            try
            {
                var roleChk = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == role.Name.ToUpper());

                if (roleChk is null)
                    return BadRequest("No Role With This Role");

                _context.Roles.Remove(roleChk);
                _context.SaveChanges();
                return Ok(await _context.Roles.ToListAsync());
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
