using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Shahajjokori.Models;

namespace Shahajjokori.Controllers
{
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        private readonly IConfiguration configuration;
        public EventController(ILogger<EventController> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create_Event()
        {
            return View();
        }
        /*
        public IActionResult Create_event_entry(Event e)
        {

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            string query = "INSERT INTO [dbo].[EVENTS] ([e_id],[e_title],[e_location],[e_opening_date],[e_closing_date],[e_exp_amount],[e_raised_amount],[e_donor_count],[e_pic],0,[e_details],[f_id]) VALUES (@title,@location,@o_date,c_date,@exp,0,0,@pic,@state,@details,@f_id)";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@title", e.e_title);
            com.Parameters.AddWithValue("@location", e.e_location);
            com.Parameters.AddWithValue("@o_date", e.e_opening_date);
            com.Parameters.AddWithValue("@c_date", e.e_closing_date);
            com.Parameters.AddWithValue("@exp", e.e_exp_amount);
            com.Parameters.AddWithValue("@raised", e.e_raised_amount);
            com.Parameters.AddWithValue("@count", e.e_donor_count);
            com.Parameters.AddWithValue("@pic", e.e_pic);
            com.Parameters.AddWithValue("@state", e.e_state);
            com.Parameters.AddWithValue("@details", e.e_details);
            com.Parameters.AddWithValue("@f_id", e.f_id);

            com.ExecuteNonQuery();
            //ViewData["Total_fundraiser"] = count;
            //connection.Close();
            return View(e);
        }*/

    }
}
