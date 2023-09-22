using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models
{
    public class Store
    {
        [Key, Column(name: "StoreID")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Store Code Is Required Field"), 
            Column(name: "StoreCode"),
            MaxLength(20, ErrorMessage = "Maximum length for the Code is 20 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Store Global Name Is Required Field"), Column(name: "StoreGName"),
            MaxLength(100, ErrorMessage = "Maximum length for the Global Name is 100 characters")]
        public string GlobalName { get; set; }

        [Required(ErrorMessage = "Store Local Name Is Required Field"), Column(name: "StoreLName"),
            MaxLength(100, ErrorMessage = "Maximum length for the Local Name is 100 characters")]
        public string LocalName { get; set; }

        //[Required(ErrorMessage = "Store Cost Center ID Is Required Field"), Column(name: "CCID")]
        public int? CostCenterID {  get; set; }

        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
