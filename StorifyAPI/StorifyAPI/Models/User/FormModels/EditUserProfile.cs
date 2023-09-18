using System.ComponentModel.DataAnnotations;

namespace StorifyAPI.Models.User.FormModels
{
    public class EditUserProfile : UserProfile
    {
        [Required]
        public string Id { get; set; }


        [Required]
        public bool IsActive { get; set; }
    }
}
