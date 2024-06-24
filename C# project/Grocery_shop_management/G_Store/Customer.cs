using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Store
{
    public class Customer
    {
        [Key]
        public int cus_id { get; set; }
        public string cus_add { get; set; }
        public string cus_contact { get; set; }
        public string due { get; set; }
    }
}
