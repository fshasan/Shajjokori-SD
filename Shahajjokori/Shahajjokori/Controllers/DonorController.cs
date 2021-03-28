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
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
namespace Shahajjokori.Controllers
{
    public class DonorController : Controller
    {
        private readonly ILogger<DonorController> _logger;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment hostingEnvironment;

        public DonorController(ILogger<DonorController> logger, IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            configuration = config;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var dnr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            ViewBag.d_name = dnr.f_name;
            ViewBag.d_id = dnr.f_id;
            //ViewBag.d_name = fr.f_id;
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            //updating events whose closing date is gone
            string query3 = "SELECT CONVERT(VARCHAR(10), getdate(),23)";
            SqlCommand com3 = new SqlCommand(query3, connection);

            string date = com3.ExecuteScalar().ToString();

            string query = $"Update EVENT set e_expired=1,e_posted=0,e_halted=0 where e_closing_date<='{date}'";
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
            string query2 = $"select * from SUCCESS_EVENT where e_state=2";

            SqlCommand com2 = new SqlCommand(query2, connection1);

            //var model = new List<Event>();
            using (SqlConnection conn = new SqlConnection(connection_string1))
            {
                conn.Open();
                SqlDataReader rdr = com2.ExecuteReader();
                while (rdr.Read())
                {
                    var e = new Event();
                    e.e_id = (int)rdr["e_id"];
                    e.e_title = (string)rdr["e_title"];
                    ViewBag.e_tilte = e.e_title;

                    e.e_location = (string)rdr["e_location"];
                    ViewBag.e_tilte = e.e_location;
                    e.e_opening_date = (string)rdr["e_opening_date"];
                    e.e_closing_date = (string)rdr["e_closing_date"];
                    e.e_exp_amount = (int)rdr["e_exp_amount"];
                    e.e_raised_amount = (int)rdr["e_raised_amount"];
                    ViewBag.e_raised_amount = e.e_raised_amount;
                    e.e_donor_count = (int)rdr["e_donor_count"];
                    ViewBag.e_donor_count = e.e_donor_count;
                    e.e_state = (int)rdr["e_state"];
                    e.e_pic = (string)rdr["e_pic"];
                    ViewBag.e_pic = e.e_pic;
                    e.e_details = (string)rdr["e_details"];
                    ViewBag.e_details = e.e_details;

                    //model.Add(e);
                }
                conn.Close();

            }
            return View(model);
        }
        public IActionResult donor_index(int id)
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            ViewBag.d_name = fr.f_name;
            ViewBag.d_id = fr.f_id;

            string connection_string1 = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection1 = new SqlConnection(connection_string1);
            connection1.Open();
            string query1 = $"select sum(amount) from DONATED where d_id = {fr.f_id}";
            SqlCommand com1 = new SqlCommand(query1, connection1);
            var count = 0;
            if(Convert.IsDBNull(com1.ExecuteScalar()))
            {
                count = 0;
            }
            else
            {
                count = (int)com1.ExecuteScalar();
            }
            ViewData["total_amount"] = count;
            //ViewData["message"] = message;
            connection1.Close();

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = $"select * from USERS where f_id = {id}";
            SqlCommand com = new SqlCommand(query, connection);


            //string connection_string1 = configuration.GetConnectionString("DefaultConnectionString");
            using (SqlConnection conn = new SqlConnection(connection_string))

            {
                conn.Open();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    var f = new Fundraiser();
                    ViewBag.f_id = (int)rdr["f_id"];
                    f.f_id = (int)rdr["f_id"];
                    f.f_email = (string)rdr["f_email"];
                    f.f_password = (string)rdr["f_password"];
                    f.f_name = (string)rdr["f_name"];
                    ViewBag.f_name = (string)rdr["f_name"];
                    return View(f);

                }
                conn.Close();

            }
            return View(fr);

        }
        public IActionResult Update_info_donor(Fundraiser fundraiser)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            var f_id = fundraiser.f_id;
            string query = $"Update USERS set f_name=@name, f_email=@email where f_id={f_id}";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@name", fundraiser.f_name);
            com.Parameters.AddWithValue("@email", fundraiser.f_email);
            com.ExecuteNonQuery();

            connection.Close();
            return RedirectToAction("donor_index", "Donor", new { id = f_id });
        }
        public IActionResult Update_info_donor_password(Fundraiser fundraiser)
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
            string f_pass = fundraiser.f_password;
            var f_id = fundraiser.f_id;
            string query1 = $"Select count(*) from USERS where f_id = {f_id} and f_password='{strBuilder.ToString()}'";
            SqlCommand com1 = new SqlCommand(query1, connection);

            var count = (int)com1.ExecuteScalar();
            if (count == 1)
            {
                MD5 md52 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text  
                md52.ComputeHash(ASCIIEncoding.ASCII.GetBytes(fundraiser.f_password1));

                //get hash result after compute it  
                byte[] result2 = md52.Hash;

                StringBuilder strBuilder2 = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits  
                    //for each byte  
                    strBuilder2.Append(result2[i].ToString("x2"));
                }
                string query = $"Update USERS set f_password=@password where f_id={f_id}";
                SqlCommand com = new SqlCommand(query, connection);
                com.Parameters.AddWithValue("@password", strBuilder2.ToString());
                com.ExecuteNonQuery();

            }
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("donor_index", "Donor", new { id = f_id });
        }
        
        //notification from fundraisers
        public IActionResult Donor_Notification(int id)
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            ViewBag.d_name = fr.f_name;
            ViewBag.d_id = fr.f_id;

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = $"select * from DONATED where d_received=1 and d_id ={id}";
            SqlCommand com = new SqlCommand(query, connection);

            var model = new List<Donation>();
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                //conn.Open();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    var d = new Donation();
                    d.d_prim = (int)rdr["e_prim"];
                    d.d_id = (int)rdr["e_id"];

                    d.d_name = (string)rdr["name"];
                    d.d_amount = (int)rdr["amount"];
                    d.d_tid = (string)rdr["tid"];
                    d.d_title = (string)rdr["e_title"];
                    d.d_amount = (int)rdr["amount"];
                    d.d_tid = (string)rdr["tid"];
                    d.d_time = (string)rdr["time"];
                    d.d_date = (string)rdr["date"];

                    model.Add(d);
                }

            }
            return View(model);
        }
        [Route("Donor/Event_section/{option}")]
        public IActionResult Event_section(string option)
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            ViewBag.d_name = fr.f_name;
            ViewBag.d_id = fr.f_id;

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            var value = 6;
            if (option == "Education")
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
        public IActionResult Event_section_show_all(int fid)
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            ViewBag.d_name = fr.f_name;
            ViewBag.d_id = fr.f_id;

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
        public IActionResult Search_Event(string search)
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            ViewBag.d_name = fr.f_name;
            ViewBag.d_id = fr.f_id;

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            string query = $"select * from EVENT where (e_posted=1 or e_posted=2) and (e_expired=0) and (e_title LIKE '%{search}%' or e_location LIKE '%{search}%')";
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
        [Route("Donor/Event_Details/{id}")]
        public IActionResult Event_Details(int id)
        {
            //Event e = new Event();
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            ViewBag.d_name = fr.f_name;
            ViewBag.d_id = fr.f_id;

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
                ViewBag.event_title = (string)dr["e_title"];
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
                string query2 = $"Select * from FUNDRAISERS where f_id = {f_id}";
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
        public IActionResult Local_event_show()
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            ViewBag.d_name = fr.f_name;
            ViewBag.d_id = fr.f_id;

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
        [Route("Donor/Event_Local_Details/{id}")]
        public IActionResult Event_Local_Details(int id)
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            ViewBag.d_name = fr.f_name;
            ViewBag.d_id = fr.f_id;
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
                e.le_location = (string)dr["le_location"];
                e.le_opening_date = (string)dr["le_opening_date"];
                e.le_closing_date = (string)dr["le_closing_date"];
                ViewBag.c_date = (string)dr["le_closing_date"];

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
        [Route("Donor/Donated/{id}")]
        public IActionResult Donated(int id)
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            ViewBag.d_name = fr.f_name;
            ViewBag.d_id = fr.f_id;

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
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            ViewBag.d_name = fr.f_name;
            ViewBag.d_id = fr.f_id;

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            
            string query1 = "SELECT CONVERT(VARCHAR(10), getdate(),105)";
            SqlCommand com1 = new SqlCommand(query1, connection);
            string date = com1.ExecuteScalar().ToString();

            string query2 = "SELECT CONVERT(VARCHAR(8),GETDATE(),108)";
            SqlCommand com2 = new SqlCommand(query2, connection);
            string time = com2.ExecuteScalar().ToString();
            
            if (donation.d_tid == null)
            {
                donation.d_tid = "Not provided";
            }
            if (donation.d_name == null)
            {
                donation.d_name = "Anonymous";
            }
            //ViewData["Total_fundraiser"] = count;
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            string query = "INSERT INTO [dbo].[DONATED]([e_id],[e_title],[amount],[tid],[name],[state],[date],[time],[d_id]) VALUES(@e_id,@e_title,@amount,@tid, @name,1,@date, @time, @d_id)";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@e_id", donation.d_id);
            com.Parameters.AddWithValue("@e_title", donation.d_title);
            com.Parameters.AddWithValue("@amount", donation.d_amount);
            com.Parameters.AddWithValue("@tid", donation.d_tid);
            com.Parameters.AddWithValue("@name", donation.d_name);
            com.Parameters.AddWithValue("@date", date);
            com.Parameters.AddWithValue("@time", time);
            com.Parameters.AddWithValue("@d_id", fr.f_id);

            com.ExecuteNonQuery();
            //ViewData["Total_fundraiser"] = count;
            //connection.Close();
            return RedirectToAction("Index", "Donor");
        }
        //kobe kothay koto taka pathaisis
        public IActionResult DonationHistory()
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            ViewBag.d_name = fr.f_name;
            ViewBag.d_id = fr.f_id;

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = $"select * from DONATED where (state=1 or state=2) and d_id = {fr.f_id} order by date, time desc";
            SqlCommand com = new SqlCommand(query, connection);

            var model = new List<Donation>();
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                //conn.Open();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    var d = new Donation();
                    d.d_prim = (int)rdr["e_prim"];
                    d.d_id = (int)rdr["e_id"];

                    d.d_name = (string)rdr["name"];
                    d.d_amount = (int)rdr["amount"];
                    d.d_tid = (string)rdr["tid"];
                    d.d_title = (string)rdr["e_title"];
                    d.d_amount = (int)rdr["amount"];
                    d.d_tid = (string)rdr["tid"];
                    d.d_time = (string)rdr["time"];
                    d.d_date = (string)rdr["date"];

                    model.Add(d);
                }

            }
            return View(model);

        }
        //history of donation
        public IActionResult Clear_Notification(int id, int fid, int amount, int prim)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query2 = $"Update DONATED set state = 3 where e_prim = {prim}";
            SqlCommand com2 = new SqlCommand(query2, connection);

            com2.ExecuteNonQuery();
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("DonationHistory", "Donor", new { id = fid });
        }
        //notification from fundraiser
        public IActionResult Cancel_Notification(int id, int fid, int prim)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query2 = $"Update DONATED set d_received = 2 where e_prim = {prim}";
            SqlCommand com2 = new SqlCommand(query2, connection);

            com2.ExecuteNonQuery();
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("Donor_Notification", "Donor", new { id = fid });
        }
        public IActionResult Log_out()
        {
            var fr = new Fundraiser() { f_id = 0, f_name = "", f_email = "", f_password = "", f_phone = "", f_about = "" };
            HttpContext.Session.SetString("FundraiserSession", JsonConvert.SerializeObject(fr));
            return RedirectToAction("Index", "Home");
        }
    }
}
