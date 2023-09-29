using Entities.Models;
using Entities.Models.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMaterialTypeRepository
    {
        IEnumerable<MaterialType> GetAllTypes(bool trackChanges);
        MaterialType GetType(Guid id, bool trackChanges);
        void CreateType(MaterialType type);
        void DeleteType(MaterialType type);

        Task<IEnumerable<MaterialType>> GetAllTypesAsync(bool trackChanges);
        Task<MaterialType> GetTypeAsync(Guid id, bool trackChanges);
    }
}
