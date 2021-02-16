using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shahajjokori.Models
{
    public class Event
    {
        public int e_id { get; set; }
        public string e_title { get; set; }
        public string e_location { get; set; }
        public string e_opening_date { get; set; }
        public string e_closing_date { get; set; }
        public int e_exp_amount { get; set; }
        public int e_raised_amount { get; set; }
        public int e_donor_count { get; set; }
        public string e_pic { get; set; }
        public int e_state { get; set; }
        public string e_details { get; set; }
        public int f_id { get; set; }

    }
}
