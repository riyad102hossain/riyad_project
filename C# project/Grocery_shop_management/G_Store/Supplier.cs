using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_Store
{
    public class Supplier
    {
        [Key]
        public int sup_id { get; set; }
        public string sup_com { get; set; }
        public int sup_contact { get; set; }
        public string sup_location { get; set; }
    }
}
