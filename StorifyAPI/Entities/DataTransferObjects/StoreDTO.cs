using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class StoreDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string GlobalName { get; set; }
        public string LocalName { get; set; }

    }
}
