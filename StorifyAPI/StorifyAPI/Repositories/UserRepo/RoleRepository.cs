using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.User.FormModels;
using System.Data;

namespace StorifyAPI.Repositories.UserRepo
{
    public class RoleRepository
    {
        private readonly IdentityContext _context;

        public RoleRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task<Task> AddAsync(RoleForm entity)
        {
            await _context.Roles.AddAsync(new IdentityRole { Name = entity.Name.Trim(), NormalizedName = entity.Name.Trim().ToUpper() });
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(IdentityRole entity)
        {
            _context.Roles.Remove(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<IdentityRole>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IdentityRole> GetByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == name.ToUpper());
        }

        public Task UpdateAsync(IdentityRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
