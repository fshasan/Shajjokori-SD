using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shahajjokori.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Shahajjokori.Models;

namespace Shahajjokori.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //private readonly ConnectionDB connectionDB;

        //public HomeController(ConnectionDB conDB)
        //{
        //    connectionDB = conDB;
        //}

        //public IEnumerable<Fundraiser> GetFundraisers { get; set; }
        //public async Task OnGet()
        //{
        //    GetFundraisers = await connectionDB.FUNDRAISERS.ToListAsync(); 
        //}
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            /*string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            //string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            string query = "Select count(*) from FUNDRAISERS";
            SqlCommand com = new SqlCommand(query, connection);
            var count = com.ExecuteScalar();
            ViewData["Total_fundraiser"] = count;
            connection.Close();*/
            return View();
        }
        public IActionResult SignUp_Fundraiser()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            //string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            string query = "Select count(*) from FUNDRAISERS";
            SqlCommand com = new SqlCommand(query, connection);
            var count = com.ExecuteScalar();
            ViewData["Total_fundraiser"] = count;
            connection.Close();
            return View();
        }

        public IActionResult SignUp_Fundraiser_entry(Fundraiser fundraiser)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            string query = "INSERT INTO [dbo].[FUNDRAISERS]([f_name],[f_email],[f_password],[f_phone],[f_about],[f_category]) VALUES(@name,@email,@password, @phone,@about,1)";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@name", fundraiser.f_name);
            com.Parameters.AddWithValue("@email", fundraiser.f_email);
            com.Parameters.AddWithValue("@password", fundraiser.f_password);
            com.Parameters.AddWithValue("@phone", fundraiser.f_phone);
            com.Parameters.AddWithValue("@about", fundraiser.f_about);

            com.ExecuteNonQuery();
            //ViewData["Total_fundraiser"] = count;
            //connection.Close();
            return View(fundraiser);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
