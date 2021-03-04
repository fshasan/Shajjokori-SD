using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shahajjokori.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using MySqlX.XDevAPI;

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
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            //var id = fr.f_id;
            string query = $"select TOP 3 * from EVENT";

            SqlCommand com = new SqlCommand(query, connection);

            var model = new List<Event>();
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    var e = new Event();
                    e.e_id = (int)rdr["e_id"];
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
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Event_section(string option)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            if (option == "Education")
            {
                string query1 = $"select * from EVENT where e_category = 0";
                SqlCommand com1 = new SqlCommand(query1, connection);

                var model1 = new List<Event>();
                using (SqlConnection conn = new SqlConnection(connection_string))
                {
                    conn.Open();
                    SqlDataReader rdr = com1.ExecuteReader();
                    while (rdr.Read())
                    {
                        var e = new Event();
                        e.e_id = (int)rdr["e_id"];
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

                        model1.Add(e);
                    }

                }

                return View(model1);
            }
            //if (option == "All")
            //{
            //    string query = $"select * from EVENT";
            //}

            /*
            SqlCommand com = new SqlCommand(query, connection);

            var model = new List<Event>();
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    var e = new Event();
                    e.e_id = (int)rdr["e_id"];
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

            }*/

            return View();
            
        }

        [Route("Home/Event_Details/{id}")]
        public IActionResult Event_Details(int id)
        {
            //Event e = new Event();
            ViewBag.event_id = id;
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = $"Select * from EVENT where e_id = {id}";
            SqlCommand com = new SqlCommand(query, connection);
            SqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                Event e = new Event();
                e.e_title = (string)dr["e_title"];
                ViewBag.event_cat = (int)dr["e_category"];
                e.e_location = (string)dr["e_location"];
                e.e_opening_date = (string)dr["e_opening_date"];
                e.e_closing_date = (string)dr["e_closing_date"];
                ViewBag.exp_amount = (int)dr["e_exp_amount"];
                ViewBag.raised_amount = (int)dr["e_raised_amount"];
                e.e_donor_count = (int)dr["e_donor_count"];
                e.e_state = (int)dr["e_state"];
                e.e_pic = (string)dr["e_pic"];
                ViewBag.event_pic = (string)dr["e_pic"];
                e.e_details = (string)dr["e_details"];
                var f_id = (int)dr["f_id"];
                connection.Close();

                string connection_string2 = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection connection2 = new SqlConnection(connection_string2);
                connection2.Open();
                string query2 = $"Select * from FUNDRAISERS where f_id = {f_id}";
                SqlCommand com2 = new SqlCommand(query2, connection2);
                SqlDataReader dr2 = com2.ExecuteReader();

                while (dr2.Read()) {
                    ViewBag.org = (string)dr2["f_name"];
                    
                }
                connection2.Close();
                return View(e);
            }
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
                ViewBag.error_message = "Email or password did not match! Try again.";
                //ViewData["error_message"] = "Email or password did not match! Try again.";
                return RedirectToAction("SignIn", "Home");
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

        

        public IActionResult Log_out()
        {
            //Microsoft.AspNetCore.Session.Abandon();
            //ApplicationLifetime.StopApplication();
            HttpContext.Session.Remove("FundraiserSession");
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
