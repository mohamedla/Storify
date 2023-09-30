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
    public class MaterialTypeRepository : RepositoryBase<MaterialType>, IMaterialTypeRepository
    {
        public MaterialTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreateEntity(MaterialType type)
            => Create(type);

        public void DeleteEntity(MaterialType type)
            => base.Delete(type);

        public IEnumerable<MaterialType> GetAllEntities(bool trackChanges)
            => FindAll(trackChanges).OrderBy(t => t.Code).ToList();

        public async Task<IEnumerable<MaterialType>> GetAllEntitiesAsync(bool trackChanges)
            => await FindAll(trackChanges).OrderBy(t => t.Code).ToListAsync();

        public MaterialType GetEntity(Guid id, bool trackChanges)
            => FindByCondition(t => t.Id.Equals(id), trackChanges).SingleOrDefault();

        public async Task<MaterialType> GetEntityAsync(Guid id, bool trackChanges)
            => await FindByCondition(t => t.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
