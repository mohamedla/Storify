using Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DataShaping
{
    public class DataShaper<T> : IDataShaper<T>
    {
        public PropertyInfo[] Properties { get; set; }

        public DataShaper()
        {
            Properties = typeof(T).GetProperties( BindingFlags.Public | BindingFlags.Instance);
        }

        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requiredProp = GetRequiredProperties(fieldsString);
            return FetchData(entities, requiredProp);
        }

        public ExpandoObject ShapeData(T entity, string fieldsString)
        {
            var requiredProp = GetRequiredProperties(fieldsString);
            return FetchObject(entity, requiredProp);
        }

        public IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString) 
        {
            if (string.IsNullOrEmpty(fieldsString))
                return Properties.ToList();

            var requiredProps = new List<PropertyInfo>();

            var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

            foreach (var field in fields)
            {
                var prop = Properties.FirstOrDefault(p => p.Name.Equals(field.Trim(), StringComparison.CurrentCultureIgnoreCase));

                if (prop == null)
                    continue;

                requiredProps.Add(prop);
            }

            return requiredProps;
        }

        public ExpandoObject FetchObject(T entity, IEnumerable<PropertyInfo> requiredProps)
        {
            var shapedObject = new ExpandoObject();

            foreach (var prop in requiredProps)
            {
                var objectPropValue = prop.GetValue(entity);
                shapedObject.TryAdd(prop.Name, objectPropValue);
            }

            return shapedObject;
        }

        public IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProps)
        {
            var shapedData = new List<ExpandoObject>();

            foreach (var entity in entities)
            {
                var shapedObject = FetchObject(entity, requiredProps);
                shapedData.Add(shapedObject);
            }

            return shapedData;
        }
    }
}
