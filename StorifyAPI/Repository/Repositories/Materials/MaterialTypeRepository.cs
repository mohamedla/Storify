using Contracts;
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

        public void CreateType(MaterialType type)
            => Create(type);

        public void DeleteType(MaterialType type)
            => Delete(type);

        public IEnumerable<MaterialType> GetAllTypes(bool trackChanges)
            => FindAll(trackChanges).OrderBy(t => t.Code).ToList();

        public async Task<IEnumerable<MaterialType>> GetAllTypesAsync(bool trackChanges)
            => await FindAll(trackChanges).OrderBy(t => t.Code).ToListAsync();

        public MaterialType GetType(Guid id, bool trackChanges)
            => FindByCondition(t => t.Id.Equals(id), trackChanges).SingleOrDefault();

        public async Task<MaterialType> GetTypeAsync(Guid id, bool trackChanges)
            => await FindByCondition(t => t.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
