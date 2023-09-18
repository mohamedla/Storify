using Microsoft.AspNetCore.Authorization;
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
    //[Authorize(Roles = RoleNames.admin)]
    public class UserRolesController : Controller
    {
        private readonly UserRepository _userRepository;

        public UserRolesController(UserManager<StoreUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = new UserRepository(userManager, roleManager);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok( await _userRepository.GetAllAsync() );
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

                return Ok(await _userRepository.GetUserRolesAsync(usr));
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

                await _userRepository.UpdateRolesAsync(model, usr);

                return RedirectToAction(nameof(GetAllUsers));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
