using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorifyAPI.Models.Matrial
{
    public class MatrialUnit : Matrial
    {
        [Key]
        [Column(name: "MUnitID")]
        public int ID { get; set; }

        public virtual ICollection<MatrialItem>? matrialItems { get; set; }

        public virtual ICollection<MatrialItemUnit>? matrialItemsUnit { get; set; }
        public virtual ICollection<MatrialItemUnit>? CmatrialItemsUnit { get; set; }
    }
}
