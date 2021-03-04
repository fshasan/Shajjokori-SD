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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Shahajjokori.Controllers
{
    
    public class FundraiserController : Controller
    {
        private readonly ILogger<FundraiserController> _logger;
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;

        public FundraiserController(ILogger<FundraiserController> logger, IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            configuration = config;
            this.hostingEnvironment = hostingEnvironment;
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
        public IActionResult Create_event_entry(PicEvent e)
        {
            string uniqueFileName = null;
            if(e.e_photo != null)
            {
                string uploadsFOlder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                //global unique identifier 
                uniqueFileName = Guid.NewGuid().ToString() + "_" + e.e_photo.FileName;
                string filePath = Path.Combine(uploadsFOlder, uniqueFileName);
                e.e_photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
        
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            
            //event pic is neglected for the time being
            string query = "INSERT INTO [dbo].[EVENT] ([e_title],[e_location],[e_opening_date],[e_closing_date],[e_exp_amount],[e_pic],[e_raised_amount],[e_donor_count],[e_state],[e_details],[f_id],[e_category]) VALUES (@title,@location,@o_date,@c_date,@exp,@pic,0,0,0,@details,@f_id, @category)";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@title", e.e_title);
            com.Parameters.AddWithValue("@location", e.e_location);
            com.Parameters.AddWithValue("@o_date", e.e_opening_date);
            com.Parameters.AddWithValue("@c_date", e.e_closing_date);
            com.Parameters.AddWithValue("@exp", e.e_exp_amount);
            //com.Parameters.AddWithValue("@raised", e.e_raised_amount);
            //com.Parameters.AddWithValue("@count", e.e_donor_count);
            com.Parameters.AddWithValue("@pic", uniqueFileName.ToString());
            //com.Parameters.AddWithValue("@state", e.e_state);
            com.Parameters.AddWithValue("@details", e.e_details);
            com.Parameters.AddWithValue("@f_id", fr.f_id);
            com.Parameters.AddWithValue("@category", e.e_category);

            com.ExecuteNonQuery();
            ViewData["e_key"] = uniqueFileName.ToString();
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return View(e);
        }

        /*public IActionResult Create_payment_entry(Payment p, string event_key)
        {
            //var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = "Select e_id from EVENT where e_pic = @pic";
            SqlCommand com = new SqlCommand(query, connection);

            com.Parameters.AddWithValue("@pic", event_key);
            //com.Parameters.AddWithValue("@password", fundraiser.f_password);

            //string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            //SqlConnection connection = new SqlConnection(connection_string);
            //connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"

            //event pic is neglected for the time being
            string query = "INSERT INTO [dbo].[EVENT] ([e_title],[e_location],[e_opening_date],[e_closing_date],[e_exp_amount],[e_pic],[e_raised_amount],[e_donor_count],[e_state],[e_details],[f_id],[e_category]) VALUES (@title,@location,@o_date,@c_date,@exp,@pic,0,0,0,@details,@f_id, @category)";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@title", e.e_title);
            com.Parameters.AddWithValue("@location", e.e_location);
            com.Parameters.AddWithValue("@o_date", e.e_opening_date);
            com.Parameters.AddWithValue("@c_date", e.e_closing_date);
            com.Parameters.AddWithValue("@exp", e.e_exp_amount);
            //com.Parameters.AddWithValue("@raised", e.e_raised_amount);
            //com.Parameters.AddWithValue("@count", e.e_donor_count);
            com.Parameters.AddWithValue("@pic", uniqueFileName.ToString());
            //com.Parameters.AddWithValue("@state", e.e_state);
            com.Parameters.AddWithValue("@details", e.e_details);
            com.Parameters.AddWithValue("@f_id", fr.f_id);
            com.Parameters.AddWithValue("@category", e.e_category);

            com.ExecuteNonQuery();
            //ViewData["Total_fundraiser"] = count;
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return View(e);
        }*/
        public IActionResult See_Events()
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            var id = fr.f_id;
            string query = $"select * from EVENT where f_id = {id}";
            
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
                    e.e_category = (int)rdr["e_category"];
                    e.e_location = (string)rdr["e_location"];
                    e.e_opening_date = (string)rdr["e_opening_date"];
                    e.e_closing_date = (string)rdr["e_closing_date"];
                    e.e_exp_amount = (int)rdr["e_exp_amount"];
                    e.e_raised_amount = (int)rdr["e_raised_amount"];
                    e.e_donor_count = (int)rdr["e_donor_count"];
                    e.e_state = (int)rdr["e_state"];
                    e.e_pic = (string)rdr["e_pic"];
                    e.e_details = (string)rdr["e_details"];

                    model.Add(e);
                }

            }

            return View(model);
        }
    }
}
