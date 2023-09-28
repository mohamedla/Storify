using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IStoreRepository _storeRepository;
        private IEmployeeRepository _employeeRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
                _repositoryContext = repositoryContext;
        }

        public IStoreRepository Store
        {
            get
            {
                if (_storeRepository == null)
                    _storeRepository = new StoreRepository(_repositoryContext);

                return _storeRepository;
            }
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);

                return _employeeRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
