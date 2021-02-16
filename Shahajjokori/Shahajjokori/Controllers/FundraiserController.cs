using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shahajjokori.Models;
using Microsoft.AspNetCore.Http;

namespace Shahajjokori.Controllers
{
    public class FundraiserController : Controller
    {
        public IActionResult Index()
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            return View(fr);
        }
    }
}
