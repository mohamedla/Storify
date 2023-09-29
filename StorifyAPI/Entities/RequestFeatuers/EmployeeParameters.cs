using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatuers
{
    public class EmployeeParameters : RequesrParameters
    {
        public EmployeeParameters()
        {
            OrderBy = "Code";
        }
        public uint MinAge { get; set; }
        public uint MaxAge { get; set; } = uint.MaxValue;

        public bool IsValidAgeRange => MaxAge > MinAge;

        public string SearchTerm {  get; set; }
    }
}
