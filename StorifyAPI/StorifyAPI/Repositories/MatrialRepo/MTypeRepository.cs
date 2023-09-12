using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.Matrial;
using System.Text.RegularExpressions;

namespace StorifyAPI.Repositories.MatrialRepo
{
    public class MTypeRepository : IMatrialRepository<MatrialType>
    {
        protected readonly StorifyContext _context;
        public MTypeRepository(StorifyContext context)
        {
            _context = context;
        }

        public async Task<MatrialType> GetByIdAsync(int id)
        {
            return await _context.Set<MatrialType>().FindAsync(id);
        }

        public async Task<IEnumerable<MatrialType>> GetAllAsync()
        {
            return await _context.Set<MatrialType>().ToListAsync();
        }

        public Task AddAsync(ref MatrialType entity)
        {
            _context.Set<MatrialType>().Add(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task UpdateAsync(MatrialType entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(MatrialType entity)
        {
            _context.Set<MatrialType>().Remove(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public bool isCodeExist(string Code)
        {
            return _context.Set<MatrialType>().Count(Type => Type.Code == Code) > 0;
        }

        public bool isIDExist(int id)
        {
            return _context.Set<MatrialType>().Count(Type => Type.ID == id) > 0;
        }
    }
}
