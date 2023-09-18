using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorifyAPI.Models.Matrial
{
    public class MatrialItemUnit
    {
        [Column(name: "MItemUnitID")]
        public int ID { get; set; }


        [Column(name: "MItemID")]
        [Required]
        public int ItemID { get; set; }

        [Column(name: "MUnitID")]
        [Required]
        public int UnitID { get; set; }

        [Column(TypeName = "Money")]
        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int CFactor { get; set; } = 1;

        [Column(name: "MCUnitID")]
        [Required]
        public int CUnitID { get; set; }

        [Required]
        public bool isMain { get; set; } = false;

        public virtual MatrialItem? MatrialItem { get; set; }
        public virtual MatrialUnit? MatrialUnit { get; set; }
        public virtual MatrialUnit? CMatrialUnit { get; set; }

    }
}
