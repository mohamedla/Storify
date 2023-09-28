using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

        public void CreateEmployeeForStore(Guid StoreId, Employee employee)
        {
            employee.StoreId = StoreId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
            => Delete(employee);

        public Employee GetEmployee(Guid StoreId, Guid Id, bool trackChanges)
            => FindByCondition(e => e.StoreId.Equals(StoreId) && e.Id.Equals(Id), trackChanges).SingleOrDefault();

        public async Task<Employee> GetEmployeeAsync(Guid StoreId, Guid Id, bool trackChanges)
            => await FindByCondition(e => e.StoreId.Equals(StoreId) && e.Id.Equals(Id), trackChanges).SingleOrDefaultAsync();

        public IEnumerable<Employee> GetEmployees(Guid StoreId, bool trackChanges)
            => FindByCondition(e => e.StoreId.Equals(StoreId), trackChanges).OrderBy(e => e.Code).ToList();

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid StoreId, bool trackChanges)
            => await FindByCondition(e => e.StoreId.Equals(StoreId), trackChanges).OrderBy(e => e.Code).ToListAsync();
    }
}
