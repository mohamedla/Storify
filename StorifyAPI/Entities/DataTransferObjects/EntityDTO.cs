using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract class EntityDTO
    {
        [Required(ErrorMessage = "Code Is Required Field"),
            MaxLength(20, ErrorMessage = "Maximum length for the Code is 20 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Global Name Is Required Field"),
           MaxLength(100, ErrorMessage = "Maximum length for the Global Name is 100 characters")]
        public string GlobalName { get; set; }

        [Required(ErrorMessage = "Local Name Is Required Field"),
            MaxLength(100, ErrorMessage = "Maximum length for the Local Name is 100 characters")]
        public string LocalName { get; set; }
    }
}
