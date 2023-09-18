using System.ComponentModel.DataAnnotations;

namespace StorifyAPI.Models.User.FormModels
{
    public class AddUserProfile : UserProfile
    {
        
        [Required]
        [StringLength(100,  MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public List<RoleModel> Roles { get; set; }
    }
}
