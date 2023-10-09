using Contracts.Material;
using Entities;
using Entities.Models;
using Entities.Models.Material;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Materials
{
    public class MaterialItemUnitRepository : RepositoryBase<MaterialItemUnit>, IMaterialItemUnitRepository
    {

        public MaterialItemUnitRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public async Task<int> ChangeItemMainUnitAsync(Guid itemId, Guid unitId)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ItemId", itemId));
            param.Add(new SqlParameter("@NewUnitId", unitId));
            param.Add(new SqlParameter("@Parameter", 1));
            var result = await Task.Run(() => _repositoryContext.Database
                .ExecuteSqlRawAsync(@"exec SP_ChangeItemMainUnit @ItemId, @NewUnitId, @Parameter", param.ToArray()));

            return result;
        }

        public async Task<int> ChangeMainItemUnitPriceAsync(Guid itemId, Guid unitId)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@ItemId", SqlDbType.UniqueIdentifier) { Value = itemId });
            param.Add(new SqlParameter("@NewUnitId", SqlDbType.UniqueIdentifier) { Value = DBNull.Value });
            param.Add(new SqlParameter("@Parameter", SqlDbType.Int) { Value = 2 });
            var result = await _repositoryContext.Database
                .ExecuteSqlRawAsync(@"exec SP_ChangeItemMainUnit @ItemId, @NewUnitId, @Parameter", param.ToArray());
            _repositoryContext.Dispose();
            return result;
        }

        public void CreateItemUnit(Guid itemId, MaterialItemUnit itemUnit)
        {
            itemUnit.ItemId = itemId;
            if (!itemUnit.IsMain)
            {
                var mainItemUnit = GetMainItemUnit(itemId, false);
                itemUnit.UnitPrice = mainItemUnit.UnitPrice * itemUnit.CFactor;
                itemUnit.LastPrice = mainItemUnit.LastPrice * itemUnit.CFactor;
                itemUnit.AveragePrice = mainItemUnit.AveragePrice * itemUnit.CFactor;
            }

            Create(itemUnit);
        }

        public void DeleteItemUnit(MaterialItemUnit itemUnit)
            => Delete(itemUnit);

        public IEnumerable<MaterialItemUnit> GetAllUnitForItem(Guid itemId, bool trackChanges)
            => FindByCondition(iu => iu.ItemId.Equals(itemId), trackChanges).OrderByDescending(iu => iu.CFactor).ToList();

        public async Task<IEnumerable<MaterialItemUnit>> GetAllUnitForItemAsync(Guid itemId, bool trackChanges)
            => await FindByCondition(iu => iu.ItemId.Equals(itemId), trackChanges).OrderByDescending(iu => iu.CFactor).ToListAsync();

        public MaterialItemUnit GetItemUnit(Guid itemId, Guid id, bool trackChanges)
            => FindByCondition(iu => iu.ItemId.Equals(itemId) && iu.Id.Equals(id), trackChanges).SingleOrDefault();

        public async Task<MaterialItemUnit> GetItemUnitAsync(Guid itemId, Guid id, bool trackChanges)
            => await FindByCondition(iu => iu.ItemId.Equals(itemId) && iu.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task<MaterialItemUnit> GetItemUnitByUnitAsync(Guid itemId, Guid unitId, bool trackChanges)
            => await FindByCondition(iu => iu.ItemId.Equals(itemId) && iu.UnitId.Equals(unitId), trackChanges).SingleOrDefaultAsync();

        public MaterialItemUnit GetMainItemUnit(Guid itemId, bool trackChanges)
         => FindByCondition(iu => iu.ItemId.Equals(itemId) && iu.IsMain.Equals(true), false).SingleOrDefault();
    }
}
