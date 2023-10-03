using Entities.Models.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Material
{
    public interface IMaterialRepository<T>
    {
        IEnumerable<T> GetAllEntities(bool trackChanges);
        T GetEntity(Guid id, bool trackChanges);
        void CreateEntity(T material);
        void DeleteEntity(T material);

        Task<IEnumerable<T>> GetAllEntitiesAsync(bool trackChanges);
        Task<T> GetEntityAsync(Guid id, bool trackChanges);
    }
}
