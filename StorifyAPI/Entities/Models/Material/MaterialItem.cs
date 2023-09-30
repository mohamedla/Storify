using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Material
{
    public class MaterialItem : Entity
    {
        [Key, Column(name: "MItemId")]
        public Guid Id { get; set; }

        [Column(name: "MGroupId"), ForeignKey(nameof(MaterialGroup)), Required(ErrorMessage = "Material Item Need To Be Linked To a Group")]
        public Guid MGroupId { get; set; }

        public virtual MaterialGroup MaterialGroup { get; set; }

    }
}
