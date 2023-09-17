using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorifyApi.FormModelsStorifyAPI.Models.User.FormModels;
using StorifyAPI.Context;
using StorifyAPI.Models.Employee;
using StorifyAPI.Models.User.FormModels;
using StorifyAPI.Repositories.UserRepo;

namespace StorifyAPI.Controllers.User
{
    [ApiController]
    [Route("api/UserRoles")]
    public class UserRolesController : Controller
    {
        private readonly UserRolesRepository _userRolesRepository;
        private readonly UserRepository _userRepository;

        public UserRolesController(UserManager<StoreUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userRolesRepository = new UserRolesRepository(userManager, roleManager);
            _userRepository = new UserRepository(userManager);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsersWithRoles()
        {
            try
            {
                return Ok( await _userRolesRepository.GetAllAsync() );
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message );
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            try
            {
                var usr = await _userRepository.GetByIdAsync(userId);
                if (usr == null)
                    return NotFound();

                return Ok(await _userRolesRepository.GetByIdAsync(usr));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserRole(UserRolesViewModel model)
        {
            try
            {
                var usr = await _userRepository.GetByIdAsync(model.UserId);
                if (usr == null)
                    return NotFound();

                await _userRolesRepository.UpdateAsync(model, usr);

                return RedirectToAction(nameof(GetAllUsersWithRoles));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
