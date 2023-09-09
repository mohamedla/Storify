using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorifyAPI.Models.Matrial
{
    public class MatrialGroup : Matrial
    {
        [Key]
        [Column(name: "MGroupID")]
        public int ID { get; set; }

        [Column(name: "MTypeID")]
        public int TypeID { get; set; }
        public virtual MatrialType? matrialType { get; set; }
        public virtual ICollection<MatrialItem>? matrialItems { get; set; }
    }
}
