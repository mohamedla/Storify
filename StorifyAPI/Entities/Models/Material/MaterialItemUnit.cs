using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Material
{
    public class MaterialItemUnit
    {
        [Key, Column(name: "MItemUnitId")]
        public Guid Id { get; set; }


        [Column(name: "MItemId"), Required(ErrorMessage = "No Item Selected"), ForeignKey(nameof(MaterialItem))]
        public Guid ItemId { get; set; }

        [Column(name: "MUnitId"), Required(ErrorMessage = "No Unit Selected"), ForeignKey(nameof(MaterialUnit))]
        public Guid UnitId { get; set; }

        [Column(TypeName = "Money")]
        public decimal UnitPrice { get; set; } // Default Price Set By The User

        [Column(TypeName = "Money")]
        public decimal AveragePrice { get; set; } // Average Price Of Different in-Stock Patches

        [Column(TypeName = "Money")]
        public decimal LastPrice { get; set; } // Last Transfer in Price 

        [Required(ErrorMessage = "No Convert Convert Factor Determined")]
        public int CFactor { get; set; }

        [Required(ErrorMessage = "Is It Main Or Not")]
        public bool IsMain { get; set; } = false;

        public virtual MaterialItem MaterialItem { get; set; }
        public virtual MaterialUnit MaterialUnit { get; set; }
    }
}
