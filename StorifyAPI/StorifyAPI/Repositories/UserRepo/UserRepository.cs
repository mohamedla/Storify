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

        public async Task<IdentityResult> AddAsync(AddUserProfile entity, IEnumerable<string> roles)
        {
            var usr = new StoreUser
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserName = entity.Username,
                Email = entity.Email
            };

            var res = await _userManager.CreateAsync(usr, entity.Password);
            if (res.Succeeded)
            {
                await _userManager.AddToRolesAsync(usr, roles);
            }
            return res;
        }

        public async Task<IdentityResult> DeactivateAsync(string Id)
        {
            var usr = await _userManager.FindByIdAsync(Id);
            if (usr == null)
                return null;
            usr.IsActive = false;
            return await _userManager.UpdateAsync(usr);
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
                IsActived = usr.IsActive,
                Roles = _userManager.GetRolesAsync(usr).Result
            }).ToListAsync();
        }

        public async Task<StoreUser> GetByIdAsync(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }

        public async Task<StoreUser> GetByEmailAsync(string Email)
        {
            return await _userManager.FindByEmailAsync(Email);
        }

        public async Task<StoreUser> GetByUsernameAsync(string Username)
        {
            return await _userManager.FindByNameAsync(Username);
        }

        public async Task<UserRolesViewModel> GetUserRolesAsync(string userId)
        {
            var usr = await this.GetByIdAsync(userId);

            if (usr == null)
                return null;

            return await _userRolesRepository.GetByUserAsync(usr);
        }

        public async Task<IdentityResult> Update(StoreUser entity, EditUserProfile newEntity)
        {

            entity.UserName = newEntity.Username;
            entity.FirstName = newEntity.FirstName;
            entity.LastName = newEntity.LastName;
            entity.Email = newEntity.Email;
            entity.IsActive = newEntity.IsActive;

            return await _userManager.UpdateAsync(entity);

        }

        public async Task<Task> UpdateRolesAsync(UserRolesViewModel entity)
        {
            var usr = await this.GetByIdAsync(entity.UserId);

            if (usr == null)
                return null;

            return await _userRolesRepository.UpdateAsync(entity, usr);
        }
    }
}
