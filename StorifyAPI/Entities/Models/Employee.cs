using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Employee
    {
        [Column("EmployeeId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Employee Code Is Required Field"),
           Column(name: "EmployeeCode"),
           MaxLength(20, ErrorMessage = "Maximum length for the Code is 20 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is a required field.")]
        public int Age { get; set; }

        [ForeignKey(nameof(Store))]
        public Guid StoreId { get; set; }

        public Store? Store { get; set; }
    }
}
