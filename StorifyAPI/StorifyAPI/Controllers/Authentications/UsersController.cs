using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using StorifyAPI.Models.Employee;
using StorifyAPI.Models.User.FormModels;
using StorifyAPI.Repositories.UserRepo;
using Entities.Models.Identity;
using Entities.DataTransferObjects.Identity;
using StorifyAPI.ActionFilters;

namespace StorifyAPI.Controllers.Authentications
{
    [ApiController]
    [Route("api/Users")]
    [Authorize(Roles = RoleNames.admin)]
    public class UsersController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UsersController(ILoggerManager loggerManager, IMapper mapper, UserManager<User> userManager)
        {
            _logger = loggerManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest( ex.Message );
            }
        }

        [HttpGet("RegisterUser")]
        [ServiceFilter(typeof(ValidationFilterAttribute))] // check model state
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDTO regUser)
        {
            var user = _mapper.Map<User>(regUser);
            
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, regUser.Roles);
            return StatusCode(201);
        }

        //[HttpPost("")]
        //public async Task<IActionResult> Add(AddUserProfile addedUser)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState.Values);

        //        if (!addedUser.Roles.Any(r => r.IsSelected))
        //            return BadRequest("Please Select At Least One Role");

        //        if (await _userRepository.GetByEmailAsync(addedUser.Email) != null)
        //            return Conflict("This Email Is Already Exists");

        //        if (await _userRepository.GetByUsernameAsync(addedUser.Username) != null)
        //            return Conflict("This Username Is Already Exists");


        //        var res = await _userRepository.AddAsync(addedUser, addedUser.Roles.Where(r => r.IsSelected).Select(r => r.RoleName));

        //        if (!res.Succeeded)
        //            return BadRequest(res.Errors);

        //        return RedirectToAction(nameof(GetAll));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpGet("EditForm/{Id}")]
        //public async Task<IActionResult> EditForm(string Id)
        //{
        //    try
        //    {
        //        var usr = await _userRepository.GetByIdAsync(Id);
        //        if (usr == null)
        //            return NotFound();

        //        var editForm = new EditUserProfile
        //        {
        //            Id = Id,
        //            Username = usr.UserName,
        //            FirstName = usr.FirstName,
        //            LastName = usr.LastName,
        //            Email = usr.Email,
        //            IsActive = usr.IsActive
        //        };


        //        return Ok(editForm);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPut("")]
        //public async Task<IActionResult> Edit(EditUserProfile editUser)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState.Values);

        //        var usr = await _userRepository.GetByIdAsync(editUser.Id);
        //        if (usr == null)
        //            return NotFound();
        //        var usrWithEmail = await _userRepository.GetByEmailAsync(editUser.Email);
        //        if (usrWithEmail != null && usrWithEmail.Id != editUser.Id) // Email has user but not this user
        //            return Conflict("This Email Is Already Exists");

        //        var usrWithName = await _userRepository.GetByUsernameAsync(editUser.Username);
        //        if (usrWithName != null && usrWithName.Id != editUser.Id)
        //            return Conflict("This Username Is Already Exists");

        //        var res = await _userRepository.Update(usr, editUser);

        //        if (!res.Succeeded)
        //            return BadRequest(res.Errors);

        //        return RedirectToAction(nameof(GetAll));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        //[HttpPatch("{Id}")]
        //public async Task<IActionResult> Deactivate(string Id)
        //{
        //    try
        //    {
        //        return await _userRepository.DeactivateAsync(Id) == null ? NotFound() : RedirectToAction(nameof(GetAll));
        //    }catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}
    }
}
