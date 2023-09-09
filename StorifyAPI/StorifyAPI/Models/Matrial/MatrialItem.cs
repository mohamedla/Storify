using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorifyAPI.Models.Matrial
{
    public class MatrialItem : Matrial
    {
        [Key]
        [Column(name: "MItemID")]
        public int ID { get; set; }

        [Column(name: "MGroupID")]
        public virtual MatrialGroup matrialGroup { get; set; }
    }
}
