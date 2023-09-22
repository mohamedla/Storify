using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        public StoreRepository( RepositoryContext repositoryContext ) : base (repositoryContext)
        { }

        public IEnumerable<Store> GetAllStores(bool trackChanges)
             => FindAll(trackChanges).OrderBy(x => x.Code).ToList();
    }
}
