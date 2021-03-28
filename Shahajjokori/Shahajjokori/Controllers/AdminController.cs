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

namespace Shahajjokori.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<FundraiserController> _logger;
        private readonly IConfiguration configuration;
        //private readonly IWebHostingEnvironment hostingEnvironment;
        private readonly IWebHostEnvironment hostingEnvironment;
        public AdminController(ILogger<FundraiserController> logger, IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            configuration = config;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult admin_index(int id)
        {
            var ad = JsonConvert.DeserializeObject<Admin>(HttpContext.Session.GetString("AdminSession"));
            
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = $"select * from ADMIN where a_id = {id}";

            SqlCommand com = new SqlCommand(query, connection);

            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    var a = new Admin();
                    a.admin_id = (int)rdr["a_id"];
                    a.admin_email = (string)rdr["a_email"];
                    a.admin_password = (string)rdr["a_password"];
                    return View(a);

                }

            }
            return View(ad);
        }
        public IActionResult Event_Request()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            string query = $"select * from EVENT where e_state = 0 and e_posted !=1 and e_halted !=1 order by e_id desc";

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
                    e.e_trans = (string)rdr["e_trans"];

                    model.Add(e);
                }

            }

            return View(model);
        }
        public IActionResult Event_state_handle_approve(int id)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = $"Update EVENT set e_posted=1, e_halted=0, e_state=1 where e_id={id}";
            SqlCommand com = new SqlCommand(query, connection);

            com.ExecuteNonQuery();

            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("Event_Request","Admin");
        }
        public IActionResult Event_state_handle_halt(int id)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"

            //event pic is neglected for the time being
            string query = $"Update EVENT set e_halted=1, e_posted=0, e_state=2 where e_id={id}";
            SqlCommand com = new SqlCommand(query, connection);

            com.ExecuteNonQuery();
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("Event_Request", "Admin");
        }     
        public IActionResult Event_state_handle_set_show_success(int id)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            string query1 = $"UPDATE SUCCESS_EVENT SET e_state=10 WHERE e_state=11";
            SqlCommand com1 = new SqlCommand(query1, connection);
            com1.ExecuteNonQuery();

            string query = $"UPDATE SUCCESS_EVENT SET e_state=11 WHERE e_id = {id}";
            SqlCommand com = new SqlCommand(query, connection);
            com.ExecuteNonQuery();

            connection.Close();
            return RedirectToAction("Event_Success", "Admin");
        }
        public IActionResult Approved_Events()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            //var id = fr.f_id;
            string query = $"select * from EVENT where (e_posted=1 or e_posted=2) and e_success not in(select e_success from EVENT where e_success=1 or e_success=2 ) and e_expired not in(select e_expired from EVENT where e_expired=1 or e_expired=2 ) order by e_id desc";

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
                    e.e_trans = (string)rdr["e_trans"];

                    model.Add(e);
                }

            }

            return View(model);
        }
        public IActionResult Halted_Events()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            //var id = fr.f_id;
            string query = $"select * from EVENT where (e_halted=1 or e_halted=2) and e_success not in(select e_success from EVENT where e_success=1 or e_success=2 ) and e_expired not in(select e_expired from EVENT where e_expired=1 or e_expired=2 ) order by e_id desc";

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
                    e.e_trans = (string)rdr["e_trans"];

                    model.Add(e);
                }

            }

            return View(model);
        }
        public IActionResult Event_Success()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            //var id = fr.f_id;
            string query = $"select * from SUCCESS_EVENT order by e_id desc";

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
                    e.e_location = (string)rdr["e_location"];
                    e.e_opening_date = (string)rdr["e_opening_date"];
                    e.e_closing_date = (string)rdr["e_closing_date"];
                    e.e_exp_amount = (int)rdr["e_exp_amount"];
                    e.e_raised_amount = (int)rdr["e_raised_amount"];
                    e.e_donor_count = (int)rdr["e_donor_count"];
                    e.e_state = (int)rdr["e_state"];
                    e.e_pic = (string)rdr["e_pic"];
                    e.e_details = (string)rdr["e_details"];
                    //e.e_trans = (string)rdr["e_trans"];

                    model.Add(e);
                }

            }

            return View(model);
        }
        public IActionResult Update_info_admin(Admin admin)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            var a_id = admin.admin_id;
            string query = $"Update ADMIN set a_email=@email where a_id={a_id}";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@email", admin.admin_email);
            com.ExecuteNonQuery();

            connection.Close();
            return RedirectToAction("admin_index", "Admin", new { id = a_id });

        }
        public IActionResult Update_info_admin_password(Admin admin)
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
            string a_pass = admin.admin_password;
            var a_id = admin.admin_id;
            string query1 = $"Select count(*) from ADMIN where a_id = {a_id} and a_password='{strBuilder.ToString()}'";
            SqlCommand com1 = new SqlCommand(query1, connection);

            var count = (int)com1.ExecuteScalar();
            if (count == 1)
            {
                MD5 md52 = new MD5CryptoServiceProvider();
                //compute hash from the bytes of text  
                md52.ComputeHash(ASCIIEncoding.ASCII.GetBytes(admin.admin_password1));
                //get hash result after compute it  
                byte[] result2 = md52.Hash;
                StringBuilder strBuilder2 = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits  
                    //for each byte  
                    strBuilder2.Append(result2[i].ToString("x2"));
                }
                string query = $"Update ADMIN set a_password=@password where a_id={a_id}";
                SqlCommand com = new SqlCommand(query, connection);
                com.Parameters.AddWithValue("@password", strBuilder2.ToString());
                com.ExecuteNonQuery();

            }
            
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("admin_index", "Admin", new { id = a_id });

        }
        public IActionResult Local_Events_Shown()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            string query1 = $"select * from LOCAL_EVENT where le_state=1 order by le_id desc";
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
        public IActionResult Local_Events_Halted()
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            string query1 = $"select * from LOCAL_EVENT where le_state=2 order by le_id desc";
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
        public IActionResult Local_Event_state_handle_approve(int id)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = $"Update LOCAL_EVENT set le_state=1 where le_id={id}";
            SqlCommand com = new SqlCommand(query, connection);

            com.ExecuteNonQuery();

            connection.Close();
            return RedirectToAction("Local_Events_Halted", "Admin");
        }
        public IActionResult Local_Event_state_handle_halt(int id)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = $"Update LOCAL_EVENT set le_state=2 where le_id={id}";
            SqlCommand com = new SqlCommand(query, connection);

            com.ExecuteNonQuery();
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("Local_Events_Shown", "Admin");
        }
        public IActionResult Edit_Info()
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
                    var a = new Info();
                    a.WhoAreWe = (string)rdr["WhoAreWe"];
                    a.HowWeWork = (string)rdr["HowWeWork"];
                    a.HelpUs = (string)rdr["HelpUs"];
                    a.Contact = (string)rdr["Contact"];
                    a.TermDonor = (string)rdr["Terms_donor"];
                    a.TermFundraiser = (string)rdr["Terms"];
                    return View(a);

                }

            }
            return View();
        }
        public IActionResult Update_general_info_admin(Info info)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = $"Update INFO set WhoAreWe=@whoarewe, HowWeWork=@howwework, HelpUs=@helpus, Contact=@contact, Terms=@fundraiser, Terms_donor=@donor";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@whoarewe", info.WhoAreWe);
            com.Parameters.AddWithValue("@howwework", info.HowWeWork);
            com.Parameters.AddWithValue("@helpus", info.HelpUs);
            com.Parameters.AddWithValue("@contact", info.Contact);
            com.Parameters.AddWithValue("@fundraiser", info.TermFundraiser);
            com.Parameters.AddWithValue("@donor", info.TermDonor);

            com.ExecuteNonQuery();

            connection.Close();
            //return View();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("Edit_Info", "Admin");
        }
    }
}
