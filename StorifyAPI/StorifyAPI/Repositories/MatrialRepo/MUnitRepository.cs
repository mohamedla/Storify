using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.Matrial;
using System.Text.RegularExpressions;

namespace StorifyAPI.Repositories.MatrialRepo
{
    public class MUnitRepository : IMatrialRepository<MatrialUnit>
    {
        protected readonly StorifyContext _context;
        public MUnitRepository(StorifyContext context)
        {
            _context = context;
        }

        public async Task<MatrialUnit> GetByIdAsync(int id)
        {
            return await _context.Set<MatrialUnit>().FirstOrDefaultAsync(I => I.ID == id);
        }

        public async Task<IEnumerable<MatrialUnit>> GetAllAsync()
        {
            return await _context.Set<MatrialUnit>().ToListAsync();
        }

        public Task AddAsync(ref MatrialUnit entity)
        {
            _context.Set<MatrialUnit>().Add(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task UpdateAsync(MatrialUnit entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(MatrialUnit entity)
        {
            _context.Set<MatrialUnit>().Remove(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public bool isCodeExist(string Code)
        {
            return _context.Set<MatrialUnit>().Count(Unit => Unit.Code == Code) > 0;
        }

        public bool isIDExist(int id)
        {
            return _context.Set<MatrialUnit>().Count(Unit => Unit.ID == id) > 0;
        }
    }
}
