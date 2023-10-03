using Contracts;
using Contracts.Material;
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
        private IMaterialItemRepository _materialItemRepository;
        private IMaterialUnitRepository _materialUnitRepository;
        private IMaterialItemUnitRepository _materialItemUnitRepository;
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

        public IMaterialItemRepository MItem
        {
            get
            {
                if (_materialItemRepository == null)
                    _materialItemRepository = new MaterialItemRepository(_repositoryContext);

                return _materialItemRepository;
            }
        }
        public IMaterialUnitRepository MUnit
        {
            get
            {
                if (_materialUnitRepository == null)
                    _materialUnitRepository = new MaterialUnitRepository(_repositoryContext);

                return _materialUnitRepository;
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

        public IMaterialItemUnitRepository MItemUnit
        {
            get
            {
                if (_materialItemUnitRepository == null)
                    _materialItemUnitRepository = new MaterialItemUnitRepository(_repositoryContext);

                return _materialItemUnitRepository;
            }
        }
        #endregion

        public void Save() => _repositoryContext.SaveChanges();

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
