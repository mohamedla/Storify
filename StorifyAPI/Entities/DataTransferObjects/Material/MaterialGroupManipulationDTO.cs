using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects.Material
{
    public class MaterialGroupManipulationDTO : EntityDTO
    {
        [Required(ErrorMessage = "Material Group Need To Be Linked To a Type")]
        public Guid MTypeId { get; set; }
    }
}
