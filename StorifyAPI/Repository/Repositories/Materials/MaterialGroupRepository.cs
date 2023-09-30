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
    public class MaterialGroupRepository : RepositoryBase<MaterialGroup>, IMaterialGroupRepository
    {
        public MaterialGroupRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreateGroup(MaterialGroup group)
            => Create(group);

        public void DeleteGroup(MaterialGroup group)
            => Delete(group);

        public IEnumerable<MaterialGroup> GetAllGroups(bool trackChanges)
            => FindAll(trackChanges).OrderBy(g => g.MTypeId).OrderBy(g => g.Code).ToList();

        public async Task<IEnumerable<MaterialGroup>> GetAllGroupsAsync(bool trackChanges)
            => await FindAll(trackChanges).OrderBy(g => g.MTypeId).OrderBy(g => g.Code).ToListAsync();

        public MaterialGroup GetGroup(Guid id, bool trackChanges)
            => FindByCondition(g => g.Id.Equals(id), trackChanges).SingleOrDefault();

        public async Task<MaterialGroup> GetGroupAsync(Guid id, bool trackChanges)
            => await FindByCondition(g => g.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }
}
