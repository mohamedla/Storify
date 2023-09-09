using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorifyAPI.Models.Matrial
{
    public class MatrialType : Matrial
    {
        [Key]
        [Column(name: "MTypeID")]
        public int ID { get; set; }

        public ICollection<MatrialGroup>? matrialGroups { get; set; }
    }
}
