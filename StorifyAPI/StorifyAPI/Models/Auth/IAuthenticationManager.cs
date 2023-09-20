using StorifyAPI.Models.User.FormModels;

namespace StorifyAPI.Models.Auth
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserLoginForm userForAuth);
        Task<string> CreateToken();
    }
}
