using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Repository.Extentions.Utility;

namespace Repository.Extentions
{
    public static class EmployeeRepositoryExtentions
    {

        public static IQueryable<Employee> FilterEmployee(this IQueryable<Employee> employees, uint minAge, uint maxAge)
            => employees.Where(e => e.Age >= minAge && e.Age <= maxAge);

        public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return employees;

            return employees.Where(e => e.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
        }

        public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByString)
        {
            if (string.IsNullOrEmpty(orderByString))
                return employees.OrderBy(e => e.Code);

            var order = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByString);

            if (string.IsNullOrEmpty(order))
                return employees.OrderBy(e => e.Code);

            return employees.OrderBy(order);
        }

    }
}
