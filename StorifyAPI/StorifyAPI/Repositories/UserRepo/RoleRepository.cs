using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.User.FormModels;
using System.Data;

namespace StorifyAPI.Repositories.UserRepo
{
    public class RoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Task> AddAsync(RoleForm entity)
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = entity.Name.Trim(), NormalizedName = entity.Name.Trim().ToUpper() });
            return Task.CompletedTask;
        }

        public async Task<Task> DeleteAsync(IdentityRole entity)
        {
            await _roleManager.DeleteAsync(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<IdentityRole>> GetAllAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IdentityRole> GetByNameAsync(string name)
        {
            return await _roleManager.Roles.FirstOrDefaultAsync(r => r.NormalizedName == name.ToUpper());
        }

        public Task UpdateAsync(IdentityRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
