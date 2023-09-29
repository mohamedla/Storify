using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatuers;
using Microsoft.EntityFrameworkCore;
using Repository.Extentions;
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

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid StoreId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.StoreId.Equals(StoreId) , trackChanges)
                .FilterEmployee(employeeParameters.MinAge, employeeParameters.MaxAge)
                .Search(employeeParameters.SearchTerm)
                .Sort(employeeParameters.OrderBy)
                .ToListAsync();

            return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
        }
    }
}
