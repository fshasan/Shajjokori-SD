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
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.Globalization;

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
        
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        [Route("Home/Index/{id}")]
        public IActionResult Index()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            
            //updating events whose closing date is gone
            string query3 = "SELECT CONVERT(VARCHAR(10), getdate(),23)";
            SqlCommand com3 = new SqlCommand(query3, connection);

            string date = com3.ExecuteScalar().ToString();

            string query = $"Update EVENT set e_expired=1, e_state=7 where e_closing_date<='{date}'";
            SqlCommand com = new SqlCommand(query, connection);

            com.ExecuteNonQuery();

            string query4 = $"Update LOCAL_EVENT set le_state=3 where le_closing_date<='{date}'";
            SqlCommand com4 = new SqlCommand(query4, connection);

            com4.ExecuteNonQuery();

            string query1 = $"select TOP 3 * from EVENT where (e_posted=1 or e_posted=2) and (e_success = 0) and (e_expired=0) order by e_id desc";

            SqlCommand com1 = new SqlCommand(query1, connection);

            var model = new List<Event>();
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

                    model.Add(e);
                }
                conn.Close();
                rdr.Close();

            }
            connection.Close();

            string connection_string1 = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection1 = new SqlConnection(connection_string1);
            connection1.Open();
            string query2 = $"select * from SUCCESS_EVENT where e_state=11";

            SqlCommand com2 = new SqlCommand(query2, connection1);

            //var model = new List<Event>();
            int dummy = 0;
            using (SqlConnection conn = new SqlConnection(connection_string1))
            {
                conn.Open();
                SqlDataReader rdr2 = com2.ExecuteReader();
                while (rdr2.Read())
                {
                    var e = new Event();
                    e.e_id = (int)rdr2["e_id"];
                    e.e_title = (string)rdr2["e_title"];
                    ViewBag.e_tilte = e.e_title;
                   
                    e.e_location = (string)rdr2["e_location"];
                    ViewBag.e_tilte = e.e_location;
                    e.e_opening_date = (string)rdr2["e_opening_date"];
                    e.e_closing_date = (string)rdr2["e_closing_date"];
                    e.e_exp_amount = (int)rdr2["e_exp_amount"];
                    e.e_raised_amount = (int)rdr2["e_raised_amount"];
                    ViewBag.e_raised_amount = e.e_raised_amount;
                    e.e_donor_count = (int)rdr2["e_donor_count"];
                    ViewBag.e_donor_count = e.e_donor_count;
                    e.e_state = (int)rdr2["e_state"];
                    e.e_pic = (string)rdr2["e_pic"];
                    ViewBag.e_pic = e.e_pic;
                    e.e_details = (string)rdr2["e_details"];
                    ViewBag.e_details = e.e_details;
                    dummy = (int)rdr2["f_id"];
                    
                    //model.Add(e);
                }
                rdr2.Close();

                conn.Close();

            }
            string query5 = $"Select * from USERS where f_id = {dummy}";
            SqlCommand com5 = new SqlCommand(query5, connection1);
            SqlDataReader dr5 = com5.ExecuteReader();

            while (dr5.Read())
            {
                ViewBag.org = (string)dr5["f_name"];
                ViewBag.org_about = (string)dr5["f_about"];
                ViewBag.org_phone = (string)dr5["f_phone"];
            }
            dr5.Close();

            return View(model);
            //return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Signin_Admin(string message)
        {
            ViewBag.error_message = message;
            return View();
        }
        public IActionResult Signin_Admin_Panel(Admin admin)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(admin.admin_password));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = "Select * from ADMIN where a_email = @email and a_password = @password";
            SqlCommand com = new SqlCommand(query, connection);

            com.Parameters.AddWithValue("@email", admin.admin_email);
            com.Parameters.AddWithValue("@password", strBuilder.ToString());

            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {
                var a_id = (int)dr["a_id"];
                var ad = new Admin() { admin_id = (int)dr["a_id"], admin_email = (string)dr["a_email"], admin_password = (string)dr["a_password"] };
                HttpContext.Session.SetString("AdminSession", JsonConvert.SerializeObject(ad));
                connection.Close();
                //return View();
                return RedirectToAction("admin_index", "Admin", new { id = a_id });
            }
            else
            {
                connection.Close();
                //ViewData["error_message"] = "Email or password did not match! Try again.";
                return RedirectToAction("Signin_Admin", "Home", new { message = "Email or password did not match! Try again." });
            }
        }
        [Route("Home/Event_section/{option}")]
        public IActionResult Event_section(string option)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            var value = 6;
            if(option == "Education")
            {
                value = 0;
            }
            if (option == "Sponsoring")
            {
                value = 1;
            }
            if (option == "Emergency Aid")
            {
                value = 2;
            }
            if (option == "Medical Care")
            {
                value = 3;
            }
            if (option == "Accidents")
            {
                value = 4;
            }
            if (option == "Others")
            {
                value = 5;
            }
            string query1 = $"select * from EVENT where e_category = {value} and (e_posted=1 or e_posted=2) and (e_success = 0) and (e_expired=0) order by e_id desc";
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
        public IActionResult Event_section_show_all()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            
            string query1 = $"select * from EVENT where (e_posted=1 or e_posted=2) and (e_success = 0) and (e_expired=0) order by e_id desc";
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
                ViewBag.event_id = (int)dr["e_id"];
                e.e_title = (string)dr["e_title"];
                ViewBag.e_title = (string)dr["e_title"];
                e.e_category = (int)dr["e_category"];
                ViewBag.event_cat = (int)dr["e_category"];
                e.e_location = (string)dr["e_location"];
                e.e_opening_date = (string)dr["e_opening_date"];
                

                e.e_closing_date = (string)dr["e_closing_date"];
                ViewBag.c_date = (string)dr["e_closing_date"];

                
                var daterenew = DateTime.ParseExact(ViewBag.c_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                ViewBag.r_days = ((daterenew - DateTime.Today).Days) + " day(s) remaining";
                
                ViewBag.exp_amount = (int)dr["e_exp_amount"];
                ViewBag.raised_amount = (int)dr["e_raised_amount"];
                e.e_donor_count = (int)dr["e_donor_count"];
                e.e_state = (int)dr["e_state"];
                e.e_pic = (string)dr["e_pic"];
                ViewBag.event_pic = (string)dr["e_pic"];
                e.e_details = (string)dr["e_details"];
                ViewBag.event_trans = (string)dr["e_trans"];
                var f_id = (int)dr["f_id"];
                connection.Close();

                string connection_string2 = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection connection2 = new SqlConnection(connection_string2);
                connection2.Open();
                string query2 = $"Select * from USERS where f_id = {f_id}";
                SqlCommand com2 = new SqlCommand(query2, connection2);
                SqlDataReader dr2 = com2.ExecuteReader();

                while (dr2.Read()) {
                    ViewBag.org = (string)dr2["f_name"];
                    ViewBag.org_about = (string)dr2["f_about"];
                    ViewBag.org_phone = (string)dr2["f_phone"];
                }
                connection2.Close();
                return View(e);
            }
            return View();
        }
        public IActionResult Local_event_show()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            string query1 = $"select * from LOCAL_EVENT where le_state=1";
            SqlCommand com1 = new SqlCommand(query1, connection);

            var model = new List<Local_Event>();
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                SqlDataReader rdr = com1.ExecuteReader();
                while (rdr.Read())
                {
                    var e = new Local_Event();
                    e.le_id = (int)rdr["le_id"];
                    e.le_title = (string)rdr["le_title"];
                    e.le_org_by = (string)rdr["le_org_by"];
                    e.le_location = (string)rdr["le_location"];
                    e.le_opening_date = (string)rdr["le_opening_date"];
                    e.le_closing_date = (string)rdr["le_closing_date"];
                    e.le_state = (int)rdr["le_state"];
                    e.le_pic = (string)rdr["le_pic"];
                    e.le_details = (string)rdr["le_details"];

                    model.Add(e);
                }

            }

            return View(model);
        }
        [Route("Home/Event_Local_Details/{id}")]
        public IActionResult Event_Local_Details(int id)
        {
            //Event e = new Event();
            ViewBag.event_id = id;
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = $"Select * from LOCAL_EVENT where le_id = {id}";
            SqlCommand com = new SqlCommand(query, connection);
            SqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                Local_Event e = new Local_Event();
                e.le_title = (string)dr["le_title"];
                ViewBag.e_title = (string)dr["le_title"];
                e.le_location = (string)dr["le_location"];
                e.le_opening_date = (string)dr["le_opening_date"];
                e.le_closing_date = (string)dr["le_closing_date"];
                ViewBag.c_date = e.le_closing_date;
                var daterenew = DateTime.ParseExact(ViewBag.c_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                ViewBag.r_days = ((daterenew - DateTime.Today).Days) + " day(s) remaining";

                e.le_state = (int)dr["le_state"];
                e.le_pic = (string)dr["le_pic"];
                ViewBag.event_pic = (string)dr["le_pic"];
                e.le_details = (string)dr["le_details"];
                e.le_org_by = (string)dr["le_org_by"];
                var f_id = (int)dr["f_id"];
                connection.Close();

                string connection_string2 = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection connection2 = new SqlConnection(connection_string2);
                connection2.Open();
                string query2 = $"Select * from USERS where f_id = {f_id}";
                SqlCommand com2 = new SqlCommand(query2, connection2);
                SqlDataReader dr2 = com2.ExecuteReader();

                while (dr2.Read())
                {
                    ViewBag.org = (string)dr2["f_name"];
                    ViewBag.org_about = (string)dr2["f_about"];
                    ViewBag.org_phone = (string)dr2["f_phone"];
                }
                connection2.Close();
                return View(e);
            }
            return View();
        }
        public IActionResult SignIn( string message)
        {
            ViewBag.error_message = message;
            return View();
        }
        public IActionResult SignIn_Panel(Fundraiser fundraiser)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(fundraiser.f_password));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = "Select * from USERS where f_email = @email and f_password = @password";
            SqlCommand com = new SqlCommand(query, connection);

            com.Parameters.AddWithValue("@email", fundraiser.f_email);
            com.Parameters.AddWithValue("@password", strBuilder.ToString());

            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
            {

                var f_id = (int)dr["f_id"];
                var cat = (int)dr["f_category"];
                if (cat == 1)
                {
                    var fr = new Fundraiser() { f_id = (int)dr["f_id"], f_name = (string)dr["f_name"], f_email = (string)dr["f_email"], f_password = (string)dr["f_password"], f_phone = (string)dr["f_phone"], f_about = (string)dr["f_about"] };
                    HttpContext.Session.SetString("FundraiserSession", JsonConvert.SerializeObject(fr));
                    connection.Close();
                    //return View();
                    return RedirectToAction("fundraiser_index", "Fundraiser", new { id = f_id });
                }
                else
                {
                    var dnr = new Fundraiser() { f_id = (int)dr["f_id"], f_name = (string)dr["f_name"], f_email = (string)dr["f_email"], f_password = (string)dr["f_password"]};
                    HttpContext.Session.SetString("FundraiserSession", JsonConvert.SerializeObject(dnr));
                    connection.Close();
                    //return View();
                    return RedirectToAction("Index", "Donor");
                }
            }

            dr.Close();
            
            
            connection.Close();
            ViewBag.error_message = "Email or password did not match! Try again.";
            //ViewData["error_message"] = "Email or password did not match! Try again.";

            return RedirectToAction("SignIn", "Home", new { message = "Email or password did not match! Try again." });
            
        }
        public IActionResult SignUp_Fundraiser(string message)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            //string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            string query = "Select count(*) from USERS";
            SqlCommand com = new SqlCommand(query, connection);

            var count = com.ExecuteScalar();
            ViewData["Total_fundraiser"] = count;
            ViewData["message"] = message;
            connection.Close();
            return View();
        }
        public IActionResult SignUp_Donor(string message)
        {

            ViewData["message"] = message;
            return View();
        }
        public IActionResult SignUp_Fundraiser_entry(Fundraiser fundraiser)
        {
            if(fundraiser.f_password != fundraiser.f_password1) {
                return RedirectToAction("SignUp_Donor", "Home", new { message = "Passwords do not match. Try again."});
            }

            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(fundraiser.f_password));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }


            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query1 = $"Select count(*) from USERS where (f_email = '{fundraiser.f_email}' or f_phone='{fundraiser.f_phone}') and f_category=1";
            SqlCommand com1 = new SqlCommand(query1, connection);

            var count = (int)com1.ExecuteScalar();
            if (count != 0)
            {
                return RedirectToAction("SignUp_Fundraiser", "Home", new { message = "Account already exists with this email or phone number!"});
            
            }
            
            //connection.Close();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            string query = "INSERT INTO [dbo].[USERS]([f_name],[f_email],[f_password],[f_phone],[f_about],[f_category]) VALUES(@name,@email,@password, @phone,@about,1)";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@name", fundraiser.f_name);
            com.Parameters.AddWithValue("@email", fundraiser.f_email);
            com.Parameters.AddWithValue("@password", strBuilder.ToString());
            com.Parameters.AddWithValue("@phone", fundraiser.f_phone);
            com.Parameters.AddWithValue("@about", fundraiser.f_about);


            com.ExecuteNonQuery();
            //ViewData["Total_fundraiser"] = count;
            connection.Close();
            return View(fundraiser);
        }
        public IActionResult SignUp_Donor_Entry(Fundraiser fundraiser)
        {
            if (fundraiser.f_password != fundraiser.f_password1)
            {
                return RedirectToAction("SignUp_Donor", "Home", new { message = "Passwords do not match. Try again." });
            }

            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(fundraiser.f_password));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }


            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query1 = $"Select count(*) from USERS where f_email = '{fundraiser.f_email}' and f_category = 2";
            SqlCommand com1 = new SqlCommand(query1, connection);

            var count = (int)com1.ExecuteScalar();
            if (count != 0)
            {
                return RedirectToAction("SignUp_Donor", "Home", new { message = "Account already exists with this email!" });

            }

            //connection.Close();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            string query = "INSERT INTO [dbo].[USERS]([f_name],[f_email],[f_password],[f_category]) VALUES(@name,@email,@password,2)";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@name", fundraiser.f_name);
            com.Parameters.AddWithValue("@email", fundraiser.f_email);
            com.Parameters.AddWithValue("@password", strBuilder.ToString());



            com.ExecuteNonQuery();
            //ViewData["Total_fundraiser"] = count;
            connection.Close();
            return View(fundraiser);

            //return RedirectToAction("donor_index", "Donor");
        }
        [Route("Home/Donated/{id}")]
        public IActionResult Donated(int id)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query1 = $"SELECT e_title from EVENT where e_id={id}";
            SqlCommand com1 = new SqlCommand(query1, connection);

            string title = com1.ExecuteScalar().ToString();

            ViewBag.e_id = id;
            ViewBag.e_title = title;
            return View();
        }
        public IActionResult Donation_entry(Donation donation)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query1 = "SELECT CONVERT(VARCHAR(10), getdate(),105)";
            SqlCommand com1 = new SqlCommand(query1, connection);

            string date = com1.ExecuteScalar().ToString();

            string query2 = "SELECT CONVERT(VARCHAR(8),GETDATE(),108)";
            SqlCommand com2 = new SqlCommand(query2, connection);

            string time = com2.ExecuteScalar().ToString();
            if (donation.d_tid == null) {
                donation.d_tid = "Not provided";
            }
            if (donation.d_name == null)
            {
                donation.d_name = "Anonymous";
            }
            //ViewData["Total_fundraiser"] = count;
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            string query = "INSERT INTO [dbo].[DONATED]([e_id],[e_title],[amount],[tid],[name],[state],[date],[time]) VALUES(@e_id,@e_title,@amount,@tid, @name,1,@date, @time)";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@e_id", donation.d_id);
            com.Parameters.AddWithValue("@e_title", donation.d_title);
            com.Parameters.AddWithValue("@amount", donation.d_amount);
            com.Parameters.AddWithValue("@tid", donation.d_tid);
            com.Parameters.AddWithValue("@name", donation.d_name);
            com.Parameters.AddWithValue("@date", date);
            com.Parameters.AddWithValue("@time", time);

            com.ExecuteNonQuery();

            return RedirectToAction("Index","Home");
        }
        public IActionResult Log_out()
        {
            //Microsoft.AspNetCore.Session.Abandon();
            //ApplicationLifetime.StopApplication();
            HttpContext.Session.Remove("FundraiserSession");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Search_Event(string search)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            string query = $"select * from EVENT where (e_posted=1 or e_posted=2) and e_success not in (Select e_success from EVENT where e_success = 1 or e_success = 2) and (e_expired=0) and (e_title LIKE '%{search}%' or e_location LIKE '%{search}%')";
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
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Success_event_show()
        {
            string connection_string1 = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection1 = new SqlConnection(connection_string1);
            connection1.Open();
            string query1 = $"select * from SUCCESS_EVENT";

            SqlCommand com1 = new SqlCommand(query1, connection1);

            var model = new List<Event>();
            using (SqlConnection conn = new SqlConnection(connection_string1))
            {
                conn.Open();
                SqlDataReader rdr = com1.ExecuteReader();
                while (rdr.Read())
                {
                    var e = new Event();
                    e.e_id = (int)rdr["e_id"];
                    e.e_title = (string)rdr["e_title"];
                    e.e_location = (string)rdr["e_location"];
                    e.e_opening_date = (string)rdr["e_opening_date"];
                    e.e_closing_date = (string)rdr["e_closing_date"];
                    //e.e_exp_amount = (int)rdr["e_exp_amount"];
                    e.e_raised_amount = (int)rdr["e_raised_amount"];
                    //ViewBag.e_raised_amount = e.e_raised_amount;
                    e.e_donor_count = (int)rdr["e_donor_count"];
                    //ViewBag.e_donor_count = e.e_donor_count;
                    e.e_state = (int)rdr["e_state"];
                    e.e_pic = (string)rdr["e_pic"];
                    e.e_details = (string)rdr["e_details"];

                    model.Add(e);
                }
                conn.Close();
            }

            return View(model);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult WhoAreWe()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            string query = $"select * from INFO";
            SqlCommand com = new SqlCommand(query, connection);

            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    ViewBag.whoarewe = (string)rdr["WhoAreWe"];
                    ViewBag.howWeWork = (string)rdr["HowWeWork"];
                    ViewBag.helpus = (string)rdr["HelpUs"];
                    ViewBag.contact = (string)rdr["Contact"];
                    
                }

            }
            return View();
        }
        public IActionResult Terms()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            string query = $"select * from INFO";
            SqlCommand com = new SqlCommand(query, connection);

            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    ViewBag.terms = (string)rdr["Terms"];
                    ViewBag.trems_don = (string)rdr["Terms_donor"];
                }

            }
            return View();
        }
    }
}
