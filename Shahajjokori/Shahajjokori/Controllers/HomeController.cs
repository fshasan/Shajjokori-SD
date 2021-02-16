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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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

        public IActionResult SignIn_Panel(Fundraiser fundraiser)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = "Select * from FUNDRAISERS where f_email = @email and f_password = @password";
            SqlCommand com = new SqlCommand(query, connection);

            com.Parameters.AddWithValue("@email", fundraiser.f_email);
            com.Parameters.AddWithValue("@password", fundraiser.f_password);

            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                /*SignInClass signIn = new SignInClass();
                signIn.id = (int)dr["f_id"];
                signIn.name = (string)dr["f_name"];
                signIn.email = (string)dr["f_email"];
                signIn.password = (string)dr["f_password"];
                signIn.phone = (string)dr["f_phone"];
                signIn.about = (string)dr["f_about"];*/
                var fr = new Fundraiser() { f_id = (int)dr["f_id"], f_name= (string)dr["f_name"], f_email= (string)dr["f_email"] , f_password= (string)dr["f_password"], f_phone= (string)dr["f_phone"], f_about= (string)dr["f_about"]};
                HttpContext.Session.SetString("FundraiserSession", JsonConvert.SerializeObject(fr));
                connection.Close();
                //return View();
                return RedirectToAction("Index", "Fundraiser");
            }
            else
            {
                connection.Close();
                return RedirectToAction("Index", "Home");
            }

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
