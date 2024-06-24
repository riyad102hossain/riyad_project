using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Store
{
    public class Supplie
    {
        [Key]
        
        public int pro_id { get; set; }

        [Key]
        
        public int sup_id { get; set; }

        public int P_Quantity { get; set; }

        [ForeignKey("pro_id")]
        public Inventory Inventory { get; set; }

        [ForeignKey("sup_id")]
        public Supplier Supplier { get; set; }
    }
}
