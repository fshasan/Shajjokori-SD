using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shahajjokori.Models;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;


namespace Shahajjokori.Controllers
{
    
    public class FundraiserController : Controller
    {
        private readonly ILogger<FundraiserController> _logger;
        private readonly IConfiguration configuration;
        public FundraiserController(ILogger<FundraiserController> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
        }
        public IActionResult Index()
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            return View(fr);
            
        }
        public IActionResult Create_Event()
        {
            return View();
        }
        public IActionResult Create_event_entry(Event e)
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
        
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            string query = "INSERT INTO [dbo].[EVENTS] ([e_title],[e_location],[e_opening_date],[e_closing_date],[e_exp_amount],[e_raised_amount],[e_donor_count],[e_pic],[e_state],[e_details],[f_id]) VALUES (@title,@location,@o_date,@c_date,@exp,0,0,@pic,0,@details,@f_id)";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@title", e.e_title);
            com.Parameters.AddWithValue("@location", e.e_location);
            com.Parameters.AddWithValue("@o_date", e.e_opening_date);
            com.Parameters.AddWithValue("@c_date", e.e_closing_date);
            com.Parameters.AddWithValue("@exp", e.e_exp_amount);
            //com.Parameters.AddWithValue("@raised", e.e_raised_amount);
            //com.Parameters.AddWithValue("@count", e.e_donor_count);
            com.Parameters.AddWithValue("@pic", e.e_pic);
            //com.Parameters.AddWithValue("@state", e.e_state);
            com.Parameters.AddWithValue("@details", e.e_details);
            com.Parameters.AddWithValue("@f_id", fr.f_id);

            com.ExecuteNonQuery();
            //ViewData["Total_fundraiser"] = count;
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return View(e);
        }

        public IActionResult See_Events()
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            var id = fr.f_id;
            string query = $"select * from EVENTS where f_id = {id}";
            
            SqlCommand com = new SqlCommand(query, connection);

            var model = new List<Event>();
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    var e = new Event();
                    e.e_title = (string)rdr["e_title"];
                    e.e_location = (string)rdr["e_location"];
                    //e.e_opening_date = rdr[];
                   // e.e_closing_date = rdr[];
                    e.e_exp_amount = (int)rdr["e_exp_amount"];
                    //e.e_raised_amount = rdr[];
                   // e.e_donor_count = rdr[];
                   // e.e_state = rdr[];
                   // e.e_pic = rdr[];
                   // e.e_details = rdr[];
                   
                    model.Add(e);
                }

            }

            return View(model);
        }
    }
}
