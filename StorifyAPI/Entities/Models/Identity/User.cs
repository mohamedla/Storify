using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;

namespace Entities.Models.Identity
{
    public class User : IdentityUser
    {
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

    }
}
