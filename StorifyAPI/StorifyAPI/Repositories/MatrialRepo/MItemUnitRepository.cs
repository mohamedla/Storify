using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.Matrial;
using System.Text.RegularExpressions;

namespace StorifyAPI.Repositories.MatrialRepo
{
    public class MItemUnitRepository : IRepository<MatrialItemUnit>
    {
        protected readonly StorifyContext _context;
        public MItemUnitRepository(StorifyContext context)
        {
            _context = context;
        }

        public async Task<MatrialItemUnit> GetByIdAsync(int id)
        {
            return await _context.Set<MatrialItemUnit>().FirstOrDefaultAsync(I => I.ID == id);
        }

        public async Task<IEnumerable<MatrialItemUnit>> GetAllAsync()
        {
            return await _context.Set<MatrialItemUnit>().ToListAsync();
        }

        public Task AddAsync(ref MatrialItemUnit entity)
        {
            _context.Set<MatrialItemUnit>().Add(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task UpdateAsync(MatrialItemUnit entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            // Ignore MatrialItemUnit Identity Column
            _context.Entry(entity).Property(MIU => MIU.ID).IsModified = false;
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(MatrialItemUnit entity)
        {
            _context.Set<MatrialItemUnit>().Remove(entity);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public bool isItemUnitExist(int ItemID, int UnitID)
        {
            return _context.Set<MatrialItemUnit>().Count(ItemUnit => ItemUnit.ItemID == ItemID && ItemUnit.UnitID == UnitID) > 0;
        }

        public bool isIDExist(int id)
        {
            return _context.Set<MatrialItemUnit>().Count(ItemUnit => ItemUnit.ID == id) > 0;
        }
    }
}
