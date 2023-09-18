using System.ComponentModel.DataAnnotations;

namespace StorifyAPI.Models.User.FormModels
{
    public class RoleForm
    {
        [Required, MaxLength(256)]
        public string Name { get; set; }
    }
}
