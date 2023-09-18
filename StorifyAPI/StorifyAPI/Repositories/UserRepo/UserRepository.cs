using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StorifyApi.FormModelsStorifyAPI.Models.User.FormModels;
using StorifyAPI.Models.Employee;
using StorifyAPI.Models.User.FormModels;
using System.Data;

namespace StorifyAPI.Repositories.UserRepo
{
    public class UserRepository
    {
        private readonly UserManager<StoreUser> _userManager;
        private readonly UserRolesRepository _userRolesRepository;

        public UserRepository(UserManager<StoreUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _userRolesRepository = new UserRolesRepository(userManager, roleManager);
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

        public async Task<StoreUser> GetByIdAsync(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }

        public async Task<UserRolesViewModel> GetUserRolesAsync(StoreUser entity)
        {
            return await _userRolesRepository.GetByUserAsync(entity);
        }

        public async Task<Task> UpdateRolesAsync(UserRolesViewModel entity, StoreUser UsrEntity)
        {
            return await _userRolesRepository.UpdateAsync(entity, UsrEntity);
        }
    }
}
