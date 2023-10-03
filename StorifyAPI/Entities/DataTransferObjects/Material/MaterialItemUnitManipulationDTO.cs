using Entities.Models.Material;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects.Material
{
    public class MaterialItemUnitManipulationDTO
    {
        [Required(ErrorMessage = "No Unit Selected"), ForeignKey(nameof(MaterialUnit))]
        public Guid UnitId { get; set; }

        public decimal? UnitPrice { get; set; }

        [Required(ErrorMessage = "No Convert Convert Factor Determined")]
        public int CFactor { get; set; }

        [Required(ErrorMessage = "Is It Main Or Not")]
        public bool IsMain { get; set; } = false;
    }
}
