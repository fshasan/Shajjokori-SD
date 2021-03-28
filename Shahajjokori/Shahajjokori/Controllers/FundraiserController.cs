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
    
    public class FundraiserController : Controller
    {
        private readonly ILogger<FundraiserController> _logger;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment hostingEnvironment;
        public FundraiserController(ILogger<FundraiserController> logger, IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            configuration = config;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult fundraiser_index(int id)
        {
            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));
            //return View(fr);

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = $"select * from USERS where f_id = {id}";

            SqlCommand com = new SqlCommand(query, connection);

            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    var f = new Fundraiser();
                    ViewBag.fun_id = (int)rdr["f_id"];
                    f.f_id = (int)rdr["f_id"];
                    f.f_email = (string)rdr["f_email"];
                    f.f_password = (string)rdr["f_password"];
                    f.f_phone = (string)rdr["f_phone"];
                    f.f_about = (string)rdr["f_about"];
                    f.f_name = (string)rdr["f_name"];
                    ViewBag.fun_name = (string)rdr["f_name"];
                    return View(f);

                }
                conn.Close();

            }

            //return RedirectToAction("Index", "Home");
            return View(fr);

        }
        [Route("Fundraiser/Create_Event/{id}")]
        public IActionResult Create_Event(int id)
        {
            return View();
        }
        public IActionResult Create_Local_Event()
        {
            return View();
        }
        public IActionResult Create_Event_Entry(PicEvent e)
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
            string query = "INSERT INTO [dbo].[EVENT] ([e_title],[e_location],[e_opening_date],[e_closing_date],[e_exp_amount],[e_pic],[e_raised_amount],[e_donor_count],[e_state],[e_details],[f_id],[e_category],[e_trans],[e_half_fund],[e_full_fund],[e_posted],[e_halted],[e_success],[e_expired]) VALUES (@title,@location,@o_date,@c_date,@exp,@pic,0,0,0,@details,@f_id, @category, @trans, 0, 0, 0, 0, 0, 0)";
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
            com.Parameters.AddWithValue("@trans", e.e_trans);

            ViewBag.f_id = fr.f_id;

            com.ExecuteNonQuery();
            //ViewData["e_key"] = uniqueFileName.ToString();
            connection.Close();
            return RedirectToAction("fundraiser_index", "Fundraiser", new { id = fr.f_id });
            //return View(e);

        }
        public IActionResult Create_local_event_entry(PicEventLocal e)
        {
            string uniqueFileName = null;
            if (e.le_photo != null)
            {
                string uploadsFOlder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                //global unique identifier 
                uniqueFileName = Guid.NewGuid().ToString() + "_" + e.le_photo.FileName;
                string filePath = Path.Combine(uploadsFOlder, uniqueFileName);
                e.le_photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"

            //event pic is neglected for the time being
            string query = "INSERT INTO [dbo].[LOCAL_EVENT] ([le_title],[le_org_by],[le_location],[le_opening_date],[le_closing_date],[le_pic],[le_state],[le_details],[f_id]) VALUES (@title, @org_by,@location,@o_date,@c_date,@pic,1,@details,@f_id)";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@title", e.le_title);
            com.Parameters.AddWithValue("@org_by", e.le_org_by);
            com.Parameters.AddWithValue("@location", e.le_location);
            com.Parameters.AddWithValue("@o_date", e.le_opening_date);
            com.Parameters.AddWithValue("@c_date", e.le_closing_date);
            //com.Parameters.AddWithValue("@raised", e.e_raised_amount);
            //com.Parameters.AddWithValue("@count", e.e_donor_count);
            com.Parameters.AddWithValue("@pic", uniqueFileName.ToString());
            //com.Parameters.AddWithValue("@state", e.e_state);
            com.Parameters.AddWithValue("@details", e.le_details);
            com.Parameters.AddWithValue("@f_id", fr.f_id);

            com.ExecuteNonQuery();
            //ViewData["e_key"] = uniqueFileName.ToString();
            connection.Close();
            //return RedirectToAction("Create_Event", "Fundraiser");
            return RedirectToAction("fundraiser_index", "Fundraiser", new { id = fr.f_id });

        }
        public IActionResult Event_state_delete(int id, int fid)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query = $"DELETE FROM EVENT WHERE e_id = {id}";
            SqlCommand com = new SqlCommand(query, connection);

            com.ExecuteNonQuery();
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("See_Events", "Fundraiser", new { id = fid});
        }
        [Route("Fundraiser/See_Events/{id}")]
        public IActionResult See_Events(int id)
        {
            //var fr = JsonConvert.DeserializeObject<Fundraiser>(HttpContext.Session.GetString("FundraiserSession"));

            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            ////string query = "SELECT [f_id],[f_name],[f_email],[f_password],[f_phone],[f_about],[f_category] FROM [dbo].[FUNDRAISERS]"
            var id1 = id;
            string query = $"select * from EVENT where f_id = {id1} order by e_id desc";
            
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
        public IActionResult Update_info_fundraiser(Fundraiser fundraiser)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            var f_id = fundraiser.f_id;
            string query = $"Update USERS set f_name=@name, f_email=@email, f_about=@about, f_phone=@phone where f_id={f_id}";
            SqlCommand com = new SqlCommand(query, connection);
            com.Parameters.AddWithValue("@name", fundraiser.f_name);
            com.Parameters.AddWithValue("@email", fundraiser.f_email);
            com.Parameters.AddWithValue("@phone", fundraiser.f_phone);
            com.Parameters.AddWithValue("@about", fundraiser.f_about);
            
            com.ExecuteNonQuery();
            
            connection.Close();
            //return View();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("fundraiser_index", "Fundraiser", new { id = f_id });
        }
        public IActionResult Update_info_fundraiser_password(Fundraiser fundraiser)
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
                string query = $"Update FUNDRAISERS set f_password=@password where f_id={f_id}";
                SqlCommand com = new SqlCommand(query, connection);
                com.Parameters.AddWithValue("@password", strBuilder2.ToString());
                com.ExecuteNonQuery();

            }
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("fundraiser_index", "Fundraiser", new { id = f_id });
        }
        [Route("Fundraiser/Notification/{id}")]
        public IActionResult Notification(int id)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            //state 0 means notification that has not been dealt with
            string query = $"select * from DONATED where state=1 and e_id in (select e_id from EVENT where f_id ={id}) order by date desc";
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
                    d.d_time= (string)rdr["time"];
                    d.d_date = (string)rdr["date"];

                    model.Add(d);
                }

            }
            return View(model);
        }
        public IActionResult Notification_from_admin(int id)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            //state 1 means approved, 2 means halted

            string query1 = $"select * from EVENT where f_id = {id} and (e_halted=1 or e_posted=1 or e_success=1 or e_expired=1 or e_half_fund=1)";
            SqlCommand com1 = new SqlCommand(query1, connection);

            var model = new List<Event>();
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                //conn.Open();
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
                    //e.e_state = (int)rdr["e_state"];
                    e.e_pic = (string)rdr["e_pic"];
                    e.e_details = (string)rdr["e_details"];
                    e.e_trans = (string)rdr["e_trans"];
                    //e.e_rev_state = (int)rdr["e_rev_state"];
                    ViewBag.e_posted = (int)rdr["e_posted"];
                    ViewBag.e_halted = (int)rdr["e_halted"];
                    ViewBag.e_expired = (int)rdr["e_expired"];
                    ViewBag.e_half_fund = (int)rdr["e_half_fund"];
                    ViewBag.e_success = (int)rdr["e_success"];
                    model.Add(e);
                }
                rdr.Close();
            }
            return View(model);
        }
        public IActionResult Received(int id, int fid, int amount, int prim)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            //increasing amount
            string query = $"Update EVENT set e_raised_amount = e_raised_amount + {amount}, e_donor_count=e_donor_count+1 where e_id = {id}";
            SqlCommand com = new SqlCommand(query, connection);
            com.ExecuteNonQuery();
           
            //setting the state of received so that donor may be notified
            string query2 = $"Update DONATED set state = 2,d_received = 1 where e_prim = {prim}";
            SqlCommand com2 = new SqlCommand(query2, connection);
            com2.ExecuteNonQuery();
            
            //e_rev_state=2 means funds half collected
            string query4 = $"Update EVENT set e_half_fund=1 where e_raised_amount>=(e_exp_amount/2) and e_id = {id} and e_half_fund != 2";

            SqlCommand com4 = new SqlCommand(query4, connection);
            com4.ExecuteNonQuery();

            //e_rev_state=3 means funds fully collected
            string query3 = $"update EVENT set e_full_fund=1,e_state=10,e_success=1 where e_exp_amount <= e_raised_amount and e_id = {id} and e_full_fund != 2";

            SqlCommand com3 = new SqlCommand(query3, connection);
            com3.ExecuteNonQuery();

            string query5 = $"INSERT INTO SUCCESS_EVENT(e_id, e_location, e_title, e_details, e_exp_amount, e_raised_amount, e_donor_count, f_id, e_opening_date, e_closing_date, e_pic, e_state) SELECT e_id, e_location, e_title, e_details, e_exp_amount, e_raised_amount, e_donor_count, f_id, e_opening_date, e_closing_date, e_pic, e_state FROM EVENT WHERE e_success=1 and f_id={fid} and e_id not in (select e_id from SUCCESS_EVENT)";

            SqlCommand com5 = new SqlCommand(query5, connection);
            com5.ExecuteNonQuery();

            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("Notification", "Fundraiser", new { id = fid });
        }      
        public IActionResult Cancel_Received(int id, int fid, int amount, int prim)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            string query2 = $"Update DONATED set state = 3 where e_prim = {prim}";
            SqlCommand com2 = new SqlCommand(query2, connection);

            com2.ExecuteNonQuery();
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("Notification", "Fundraiser", new { id = fid });
        }
        public IActionResult Cancel_Admin_Notification(int id, int fid, int aid)
        {
            string connection_string = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();
            //e_rev_state=1 means notification has been showed and fundraiser cancelled it
            if (aid == 1)
            {
                string query1 = $"Update EVENT set e_posted = 2 where e_id = {id}";
                SqlCommand com1 = new SqlCommand(query1, connection);
                com1.ExecuteNonQuery();
            }
            if (aid == 2)
            {
                string query1 = $"Update EVENT set e_halted = 2 where e_id = {id}";
                SqlCommand com1 = new SqlCommand(query1, connection);
                com1.ExecuteNonQuery();
            }
            if (aid == 3)
            {
                string query1 = $"Update EVENT set e_half_fund = 2 where e_id = {id}";
                SqlCommand com1 = new SqlCommand(query1, connection);
                com1.ExecuteNonQuery();
            }
            if (aid == 4)
            {
                string query1 = $"Update EVENT set e_success = 2 where e_id = {id}";
                SqlCommand com1 = new SqlCommand(query1, connection);
                com1.ExecuteNonQuery();
            }
            if (aid == 5)
            {
                string query1 = $"Update EVENT set e_expired = 2 where e_id = {id}";
                SqlCommand com1 = new SqlCommand(query1, connection);
                com1.ExecuteNonQuery();
            }
            connection.Close();
            //return RedirectToAction("Create_event_entry","Fundraiser");
            return RedirectToAction("Notification_from_admin", "Fundraiser", new { id = fid });
        }
        
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Log_out()
        {
            var fr = new Fundraiser() { f_id = 0, f_name = "", f_email = "", f_password = "", f_phone = "", f_about = "" };
            HttpContext.Session.SetString("FundraiserSession", JsonConvert.SerializeObject(fr));
            return RedirectToAction("Index", "Home");
        }
    }
}
