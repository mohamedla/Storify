using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Material;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IStoreRepository Store { get; }

        IEmployeeRepository Employee { get; }

        #region Material
        IMaterialTypeRepository MType { get; }

        IMaterialGroupRepository MGroup { get; }

        IMaterialItemRepository MItem { get; } 
        #endregion

        void Save();
        Task SaveAsync();

    }
}
