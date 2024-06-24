using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Store
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        public int cus_id { get; set; }
        public int pro_id { get; set; }
        public DateTime S_Date { get; set; }

        
        public int P_Quantity { get; set; }

        [ForeignKey("cus_id")]
        public Customer Customer { get; set; }

        [ForeignKey("pro_id")]
        public Inventory Inventory { get; set; }

        public ICollection<Supplie> Supplies { get; set; }

    }
}
