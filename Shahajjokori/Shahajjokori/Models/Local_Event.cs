using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shahajjokori.Models
{
    public class Local_Event
    {
        public int le_id { get; set; }
        public string le_title { get; set; }

        public string le_org_by { get; set; }

        public string le_location { get; set; }
        public string le_opening_date { get; set; }
        public string le_closing_date { get; set; }
        public string le_pic { get; set; }
        public int le_state { get; set; }
        public string le_details { get; set; }
        public int f_id { get; set; }

    }
}

