using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.Employee;
using StorifyAPI.Models.User.FormModels;
using StorifyAPI.Repositories.UserRepo;
using System.Data;

namespace StorifyAPI.Controllers.User
{
    [ApiController]
    [Route("api/Roles")]
    //[Authorize(Roles = RoleNames.admin)]
    public class RolesController : Controller
    {
        private readonly RoleRepository _roleRepository;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleRepository = new RoleRepository(roleManager);
        }
        
        private async Task<IActionResult> GetRoleList()
        {
            return Ok((from r in await _roleRepository.GetAllAsync() select new RoleModel { RoleId = r.Id, RoleName =  r.Name }));
        }

        [HttpGet("")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAll()
        {
            return await GetRoleList();
        }

        [HttpPost("")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleForm role)
        {
            if (!ModelState.IsValid)
                return BadRequest("Role Name Not Valid");
            try
            {
                var roleChk = await _roleRepository.GetByNameAsync(role.Name);

                if ( roleChk is not null)
                    return BadRequest("The Name Already Exist");

                await _roleRepository.AddAsync(role);

                return await GetRoleList();

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
                var roleChk = await _roleRepository.GetByNameAsync(role.Name);

                if (roleChk is null)
                    return BadRequest("No Role With This Role");

                await _roleRepository.DeleteAsync(roleChk);
                return await GetRoleList();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
