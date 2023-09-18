using Microsoft.AspNetCore.Identity;
//using StorifyAPI.Models.Stores;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorifyAPI.Models.Employee
{
    public class StoreUser : IdentityUser
    {
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string LastName { get; set; }

        public byte[]? PersonalImage { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

    }
}
