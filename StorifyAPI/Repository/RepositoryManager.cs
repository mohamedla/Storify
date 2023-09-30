using Contracts;
using Entities;
using Repository.Repositories.Materials;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IStoreRepository _storeRepository;
        private IEmployeeRepository _employeeRepository;
        #region Material
        private IMaterialTypeRepository _materialTypeRepository;
        private IMaterialGroupRepository _materialGroupRepository; 
        #endregion

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

        #region Material Repos
        public IMaterialTypeRepository MType
        {
            get
            {
                if (_materialTypeRepository == null)
                    _materialTypeRepository = new MaterialTypeRepository(_repositoryContext);

                return _materialTypeRepository;
            }
        }

        public IMaterialGroupRepository MGroup
        {
            get
            {
                if (_materialGroupRepository == null)
                    _materialGroupRepository = new MaterialGroupRepository(_repositoryContext);

                return _materialGroupRepository;
            }
        } 
        #endregion

        public void Save() => _repositoryContext.SaveChanges();

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
