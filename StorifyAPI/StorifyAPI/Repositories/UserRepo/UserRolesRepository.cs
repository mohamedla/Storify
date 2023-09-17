using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StorifyApi.FormModelsStorifyAPI.Models.User.FormModels;
using StorifyAPI.Models.Employee;
using StorifyAPI.Models.User.FormModels;
using System.Data;

namespace StorifyAPI.Repositories.UserRepo
{
    public class UserRolesRepository
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesRepository(UserManager<StoreUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Task> AddAsync(RoleForm entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityRole entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            return await _userManager.Users.Select(usr => new UserViewModel
            {
                Id = usr.Id,
                UserName = usr.UserName,
                FirstName = usr.FirstName,
                LastName = usr.LastName,
                Email = usr.Email,
                Roles = _userManager.GetRolesAsync(usr).Result
            }).ToListAsync();
        }

        public async Task<UserRolesViewModel> GetByIdAsync(StoreUser entity)
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return new UserRolesViewModel
            {
                UserId = entity.Id,
                USerName = entity.UserName,
                Roles = roles.Select(r => new RoleModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(entity, r.Name).Result
                }).ToList()
            };
        }

        public async Task<Task> UpdateAsync(UserRolesViewModel entity, StoreUser UsrEntity)
        {
            var userRoles = await _userManager.GetRolesAsync(UsrEntity);

            foreach (var role in entity.Roles)
            {
                if (role.IsSelected && !userRoles.Any(r => r == role.RoleName))
                    await _userManager.AddToRoleAsync(UsrEntity, role.RoleName);

                if (!role.IsSelected && userRoles.Any(r => r == role.RoleName))
                    await _userManager.RemoveFromRoleAsync(UsrEntity, role.RoleName);
            }

            return Task.CompletedTask;
        }
    }
}
