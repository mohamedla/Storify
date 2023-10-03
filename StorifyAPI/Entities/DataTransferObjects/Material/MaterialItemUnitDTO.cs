using Entities.Models.Material;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects.Material
{
    public class MaterialItemUnitDTO : MaterialItemUnitManipulationDTO
    {
        public Guid Id { get; set; }

        public decimal AveragePrice { get; set; } 

        public decimal LastPrice { get; set; } 
    }
}
