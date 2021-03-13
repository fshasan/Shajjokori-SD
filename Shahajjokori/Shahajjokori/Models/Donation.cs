using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shahajjokori.Models
{
    public class Donation
    {
        public int d_prim { get; set; }
        public int d_id { get; set; }
        public string d_name { get; set; }
        public string d_title { get; set; }
        public int d_amount { get; set; }
        public string d_date { get; set; }

        public string d_time { get; set; }
        public string d_tid { get; set; }
    }
}
