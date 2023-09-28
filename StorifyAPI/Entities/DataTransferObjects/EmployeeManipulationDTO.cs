using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract class EmployeeManipulationDTO
    {
        [Required(ErrorMessage = "Employee Code Is Required Field"),
           MaxLength(20, ErrorMessage = "Maximum length for the Code is 20 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is a required field."), Range(18, 90, ErrorMessage = "The Age Should Be Between 18 and 90")]
        public int Age { get; set; }
    }
}
