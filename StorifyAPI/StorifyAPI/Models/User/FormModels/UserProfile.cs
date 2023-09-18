using System.ComponentModel.DataAnnotations;

namespace StorifyAPI.Models.User.FormModels
{
    public class UserProfile
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Frist Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Username")]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}
