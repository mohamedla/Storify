using Entities.Models.Material;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects.Material
{
    public class MaterialItemUnitUpdateDTO
    {
        public decimal? UnitPrice { get; set; }
    }
}
