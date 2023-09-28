﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(Guid StoreId, bool trackChanges);
        Employee GetEmployee(Guid StoreId, Guid Id, bool trackChanges);
        void CreateEmployeeForStore(Guid StoreId, Employee employee);
        void DeleteEmployee(Employee employee);

        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid StoreId, bool trackChanges);
        Task<Employee> GetEmployeeAsync(Guid StoreId, Guid Id, bool trackChanges);
    }
}
