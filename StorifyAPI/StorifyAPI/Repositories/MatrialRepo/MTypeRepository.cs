using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.Matrial;
using System.Text.RegularExpressions;

namespace StorifyAPI.Repositories.MatrialRepo
{
    public class MTypeRepository : IMatrialRepository<MartialType>
    {
        protected readonly StorifyContext _context;
        public MTypeRepository(StorifyContext context)
        {
            _context = context;
        }

        public async Task<MartialType> GetByIdAsync(int id)
        {
            return await _context.Set<MartialType>().FindAsync(id);
        }

        public async Task<IEnumerable<MartialType>> GetAllAsync()
        {
            return await _context.Set<MartialType>().ToListAsync();
        }

        public Task AddAsync(ref MartialType entity)
        {
            _context.Set<MartialType>().Add(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task UpdateAsync(MartialType entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(MartialType entity)
        {
            _context.Set<MartialType>().Remove(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public bool isCodeExist(string Code)
        {
            return _context.Set<MartialType>().Count(Type => Type.Code == Code) > 0;
        }

        public bool isIDExist(int id)
        {
            return _context.Set<MartialType>().Count(Type => Type.ID == id) > 0;
        }
    }
}
