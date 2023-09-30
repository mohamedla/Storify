using Entities.Models;
using Entities.Models.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IMaterialGroupRepository
    {
        IEnumerable<MaterialGroup> GetAllGroups(bool trackChanges);
        MaterialGroup GetGroup(Guid id, bool trackChanges);
        void CreateGroup(MaterialGroup group);
        void DeleteGroup(MaterialGroup group);

        Task<IEnumerable<MaterialGroup>> GetAllGroupsAsync(bool trackChanges);
        Task<MaterialGroup> GetGroupAsync(Guid id, bool trackChanges);
    }
}
