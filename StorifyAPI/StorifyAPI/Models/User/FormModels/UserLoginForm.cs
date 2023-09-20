using System.ComponentModel.DataAnnotations;

namespace StorifyAPI.Models.User.FormModels
{
    public class UserLoginForm
    {
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password name is required")]
        public string Password { get; set; }

    }
}
