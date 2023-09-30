using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects.Material
{
    public class MaterialItemDTO : EntityDTO
    {
        public Guid Id { get; set; }

        public Guid MGroupId { get; set; }
    }
}
