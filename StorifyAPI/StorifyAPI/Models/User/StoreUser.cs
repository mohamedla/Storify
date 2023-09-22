using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;

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

        public Guid StoreId {  get; set; }

        public virtual Store?  Store { get; set; }

    }
}
