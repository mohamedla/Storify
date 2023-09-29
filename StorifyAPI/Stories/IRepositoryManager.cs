using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IStoreRepository Store { get; }

        IEmployeeRepository Employee { get; }

        IMaterialTypeRepository MType { get; }

        void Save();
        Task SaveAsync();

    }
}
