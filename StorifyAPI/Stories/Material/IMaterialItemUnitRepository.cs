using Entities.Models;
using Entities.Models.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Material
{
    public interface IMaterialItemUnitRepository
    {
        IEnumerable<MaterialItemUnit> GetAllUnitForItem(Guid itemId, bool trackChanges);
        MaterialItemUnit GetItemUnit(Guid itemId, Guid id, bool trackChanges);
        void CreateItemUnit(Guid itemId, MaterialItemUnit itemUnit);
        void DeleteItemUnit(MaterialItemUnit itemUnit);

        Task<IEnumerable<MaterialItemUnit>> GetAllUnitForItemAsync(Guid itemId, bool trackChanges);
        Task<MaterialItemUnit> GetItemUnitAsync(Guid itemId, Guid id, bool trackChanges);
        Task<MaterialItemUnit> GetItemUnitByUnitAsync(Guid itemId, Guid unitId, bool trackChanges);
        MaterialItemUnit GetMainItemUnit(Guid itemId, bool trackChanges);
        Task<int> ChangeItemMainUnitAsync(Guid itemId, Guid unitId);
        Task<int> ChangeMainItemUnitPriceAsync(Guid itemId, Guid unitId);
    }
}
