using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Material
{
    public class MaterialType : Entity
    {
        [Key, Column(name: "MTypeID")]
        public Guid Id { get; set; }

        public virtual IEnumerable<MaterialGroup>? MaterialGroups { get; set; }

    }
}
