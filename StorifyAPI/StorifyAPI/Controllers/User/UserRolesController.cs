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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            try
            {
                var usrRoles = await _userRepository.GetUserRolesAsync(userId);

                if (usrRoles == null) 
                    return NotFound();

                return Ok(usrRoles);
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
                var task = await _userRepository.UpdateRolesAsync(model);

                if (task == null)
                    NotFound();

                return RedirectToAction(nameof(UsersController.GetAll), "Users");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
