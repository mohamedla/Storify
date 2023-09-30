using Contracts.Material;
using Entities;
using Entities.Models;
using Entities.Models.Material;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Materials
{
    public class MaterialItemRepository : RepositoryBase<MaterialItem>, IMaterialItemRepository
    {
        public MaterialItemRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreateEntity(MaterialItem item)
            => Create(item);

        public void DeleteEntity(MaterialItem item)
            => Delete(item);

        public IEnumerable<MaterialItem> GetAllEntities(bool trackChanges)
            => FindAll(trackChanges).OrderBy(g => g.MGroupId).OrderBy(g => g.Code).ToList();

        public async Task<IEnumerable<MaterialItem>> GetAllEntitiesAsync(bool trackChanges)
            => await FindAll(trackChanges).OrderBy(g => g.MGroupId).OrderBy(g => g.Code).ToListAsync();

        public MaterialItem GetEntity(Guid id, bool trackChanges)
            => FindByCondition(g => g.Id.Equals(id), trackChanges).SingleOrDefault();

        public async Task<MaterialItem> GetEntityAsync(Guid id, bool trackChanges)
            => await FindByCondition(g => g.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
