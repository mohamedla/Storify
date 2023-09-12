using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorifyAPI.Models.Matrial
{
    public class MatrialItem : Matrial
    {
        [Key]
        [Column(name: "MItemID")]
        public int ID { get; set; }

        public ICollection<MatrialUnit>? matrialUnits { get; set; }
        public ICollection<MatrialItemUnit>? matrialItemUnits { get; set; }

        [Column(name: "MGroupID")]
        [Required]
        public int GroupID { get; set; }
        public virtual MatrialGroup? matrialGroup { get; set; }
    }
}
