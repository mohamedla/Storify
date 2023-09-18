//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

//namespace StorifyAPI.Models.Stores
//{
//    public class Storage : Matrial.Matrial
//    {

//        [Key]
//        [Column(name: "StorageID")]
//        public int ID { get; set; }

//        public int TypeID { get; set; }

//        public int CostCenterID { get; set; }

//        // Storekeepers
//        // Items, Groups

//        public virtual CostCenter CostCenter { get; set; }
//        public virtual ICollection<Employee.Employee> Storekeepers { get; set; }
//    }
//}
