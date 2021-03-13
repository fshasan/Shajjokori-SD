using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shahajjokori.Models
{
    public class Admin
    {
        public int admin_id { get; set; }
        public string admin_email{ get; set; }

        public string admin_password { get; set; }
        public string admin_password1 { get; set; }
    }
}
