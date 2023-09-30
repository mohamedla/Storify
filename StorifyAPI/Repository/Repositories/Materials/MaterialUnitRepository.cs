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
    public class MaterialUnitRepository : RepositoryBase<MaterialUnit>, IMaterialUnitRepository
    {
        public MaterialUnitRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreateEntity(MaterialUnit unit)
            => Create(unit);

        public void DeleteEntity(MaterialUnit unit)
            => Delete(unit);

        public IEnumerable<MaterialUnit> GetAllEntities(bool trackChanges)
            => FindAll(trackChanges).OrderBy(g => g.Code).ToList();

        public async Task<IEnumerable<MaterialUnit>> GetAllEntitiesAsync(bool trackChanges)
            => await FindAll(trackChanges).OrderBy(g => g.Code).ToListAsync();

        public MaterialUnit GetEntity(Guid id, bool trackChanges)
            => FindByCondition(g => g.Id.Equals(id), trackChanges).SingleOrDefault();

        public async Task<MaterialUnit> GetEntityAsync(Guid id, bool trackChanges)
            => await FindByCondition(g => g.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
