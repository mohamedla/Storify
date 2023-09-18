using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.Matrial;
using System.Text.RegularExpressions;

namespace StorifyAPI.Repositories.MatrialRepo
{
    public class MGroupRepository : IMatrialRepository<MatrialGroup>
    {
        protected readonly StorifyContext _context;
        public MGroupRepository(StorifyContext context)
        {
            _context = context;
        }

        public async Task<MatrialGroup> GetByIdAsync(int id)
        {
            return await _context.Set<MatrialGroup>().Include("matrialType").FirstOrDefaultAsync(g => g.ID == id);
        }

        public async Task<IEnumerable<MatrialGroup>> GetAllAsync()
        {
            return await _context.Set<MatrialGroup>().Include("matrialType").ToListAsync();
        }

        public Task AddAsync(ref MatrialGroup entity)
        {
            _context.Set<MatrialGroup>().Add(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task UpdateAsync(MatrialGroup entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(MatrialGroup entity)
        {
            _context.Set<MatrialGroup>().Remove(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public bool isCodeExist(string Code)
        {
            return _context.Set<MatrialGroup>().Count(Group => Group.Code == Code) > 0;
        }

        public bool isIDExist(int id)
        {
            return _context.Set<MatrialGroup>().Count(Group => Group.ID == id) > 0;
        }
    }
}
