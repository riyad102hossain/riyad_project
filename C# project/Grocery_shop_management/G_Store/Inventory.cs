using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Store
{
    public class Inventory
    {
        [Key]
        public int pro_id { get; set; }
        public string pro_cat { get; set; }
        public DateTime doe { get; set; }
        public decimal selling_price { get; set; }
    }
}
