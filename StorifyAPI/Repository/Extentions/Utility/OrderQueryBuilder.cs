using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extentions.Utility
{
    public static class OrderQueryBuilder
    {
        public static string CreateOrderQuery<T>(string orderByString)
        {
            var orderParams = orderByString.Trim().Split(',');

            var props = typeof(Employee).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrEmpty(param))
                    continue;

                var propName = param.Split(" ")[0];

                var objProp = props.FirstOrDefault(p => p.Name.Equals(propName, StringComparison.InvariantCultureIgnoreCase));

                if (objProp == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderBuilder.Append($"{objProp.Name.ToString()} {direction} , ");

            }

            return orderBuilder.ToString().TrimEnd(',', ' ');
        }

    }
}
