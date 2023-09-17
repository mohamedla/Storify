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

        public UserRepository(UserManager<StoreUser> userManager)
        {
            _userManager = userManager;
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
            throw new NotImplementedException();
        }

        public async Task<StoreUser> GetByIdAsync(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }

        public async Task<Task> UpdateAsync(UserRolesViewModel entity, StoreUser UsrEntity)
        {
            throw new NotImplementedException();
        }
    }
}
