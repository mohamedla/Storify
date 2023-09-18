using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorifyAPI.Models.Matrial
{
    public class Matrial
    {
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string Code { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(200)]
        public string GlobalName { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(200)]
        public string LocalName { get; set; }
    }
}
