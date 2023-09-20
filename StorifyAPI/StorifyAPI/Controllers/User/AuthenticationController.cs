using Microsoft.AspNetCore.Mvc;
using StorifyAPI.Models.Auth;
using StorifyAPI.Models.User.FormModels;

namespace StorifyAPI.Controllers.User
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationManager _authManager;
        public AuthenticationController(IAuthenticationManager authManager) {
            _authManager = authManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginForm user)
        {
            if (!await _authManager.ValidateUser(user))
                return Unauthorized();
            
            return Ok(new { Token = await _authManager.CreateToken() });
        }
    }
}
