using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Shahajjokori.Models
{
    public class Fundraiser
    {
        public int f_id { get; set; }
        public string f_name { get; set; }
        public string f_email { get; set; }
        public string f_password { get; set; }
        public string f_password1 { get; set; }
        public string f_phone { get; set; }
        public string f_about { get; set; }
        public int f_category { get; set; }
    }
}
