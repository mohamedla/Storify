using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class StoreUpdateDTO
    {
        public string Code { get; set; }
        public string GlobalName { get; set; }
        public string LocalName { get; set; }
        public IEnumerable<EmployeeCreateDTO>? Employees { get; set; }
    }
}
