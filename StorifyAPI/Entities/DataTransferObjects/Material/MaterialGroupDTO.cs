using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects.Material
{
    public class MaterialGroupDTO : EntityDTO
    {
        public Guid Id { get; set; }

        public Guid MTypeId { get; set; }
    }
}
