using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IStoreRepository
    {
        IEnumerable<Store> GetAllStores(bool trackChanges);
        Store GetStore(Guid id, bool trackChanges);
        IEnumerable<Store> GetStoresByIds(IEnumerable<Guid> Ids, bool trackChanges);
        void CreateStore(Store store);
        void DeleteStore(Store store);

        Task<IEnumerable<Store>> GetAllStoresAsync(bool trackChanges);
        Task<Store> GetStoreAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Store>> GetStoresByIdsAsync(IEnumerable<Guid> Ids, bool trackChanges);
    }
}
