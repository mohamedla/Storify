using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Material
{
    public class MaterialGroup : Entity
    {
        [Key, Column(name: "MGroupId")]
        public Guid Id { get; set; }

        [Column(name: "MTypeId"), ForeignKey(nameof(MaterialType)), Required(ErrorMessage = "Material Group Need To Be Linked To a Type")]
        public Guid MTypeId { get; set; }

        public virtual MaterialType MaterialType { get; set; }

        public virtual IEnumerable<MaterialItem>? MaterialItems { get; set; }

    }
}
