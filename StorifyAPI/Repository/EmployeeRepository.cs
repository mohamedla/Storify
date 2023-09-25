using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee> , IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public Employee GetEmployee(Guid StoreId, Guid Id, bool trackChanges)
            => FindByCondition(e => e.StoreId.Equals(StoreId) && e.Id.Equals(Id), trackChanges).SingleOrDefault();
        public IEnumerable<Employee> GetEmployees(Guid StoreId, bool trackChanges)
            => FindByCondition(e => e.StoreId.Equals(StoreId), trackChanges).OrderBy(e => e.Code);
    }
}
