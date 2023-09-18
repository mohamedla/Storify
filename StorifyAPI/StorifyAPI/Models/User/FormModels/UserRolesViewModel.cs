using StorifyAPI.Models.User.FormModels;

namespace StorifyApi.FormModelsStorifyAPI.Models.User.FormModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string USerName { get; set; }
        public List<RoleModel> Roles { get; set; }
    }
}
