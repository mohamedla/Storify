using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.Matrial;
using System.Text.RegularExpressions;

namespace StorifyAPI.Repositories.MatrialRepo
{
    public class MItemRepository : IMatrialRepository<MatrialItem>
    {
        protected readonly StorifyContext _context;
        public MItemRepository(StorifyContext context)
        {
            _context = context;
        }

        public async Task<MatrialItem> GetByIdAsync(int id)
        {
            return await _context.Set<MatrialItem>().Include("matrialGroup").FirstOrDefaultAsync(I => I.ID == id);
        }

        public async Task<IEnumerable<MatrialItem>> GetAllAsync()
        {
            return await _context.Set<MatrialItem>().Include("matrialGroup").ToListAsync();
        }

        public Task AddAsync(ref MatrialItem entity)
        {
            _context.Set<MatrialItem>().Add(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task UpdateAsync(MatrialItem entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(MatrialItem entity)
        {
            _context.Set<MatrialItem>().Remove(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public bool isCodeExist(string Code)
        {
            return _context.Set<MatrialItem>().Count(Group => Group.Code == Code) > 0;
        }

        public bool isIDExist(int id)
        {
            return _context.Set<MatrialItem>().Count(Group => Group.ID == id) > 0;
        }
    }
}
