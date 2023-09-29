using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorifyAPI.Models.Matrial
{
    public class MartialType : Matrial
    {
        [Key]
        [Column(name: "MTypeID")]
        public int ID { get; set; }

        public virtual ICollection<MatrialGroup>? matrialGroups { get; set; }
    }
}
