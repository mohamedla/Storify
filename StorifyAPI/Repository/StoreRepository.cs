﻿using Contracts;
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

        public void CreateStore(Store store)
            => Create(store);

        public IEnumerable<Store> GetAllStores(bool trackChanges)
             => FindAll(trackChanges).OrderBy(x => x.Code).ToList();

        public IEnumerable<Store> GetStoresByIds(IEnumerable<Guid> Ids, bool trackChanges)
            => FindByCondition(s => Ids.Contains(s.Id), trackChanges).ToList();

        public Store GetStore(Guid id, bool trackChanges)
            => FindByCondition(s => s.Id.Equals(id), trackChanges).SingleOrDefault();

        public void DeleteStore(Store store)
            => Delete(store);
    }
}
