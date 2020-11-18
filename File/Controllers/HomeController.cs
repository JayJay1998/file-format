using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using File.Models;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;  
using System.Configuration;  

namespace File.Controllers
{
    public class HomeController : Controller
    {
        RecordContext dbh = null;
        FileFormat funx = null;

        public HomeController()
        {
            dbh = new RecordContext();
            funx = new FileFormat();
        }
        public ActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Firstname, string Lastname, HttpPostedFileBase UploadFile)
        {

            if (UploadFile != null)
            {
                string fileName = Path.GetFileName(UploadFile.FileName);
                if (UploadFile.ContentLength < 104857600)
                {
                    UploadFile.SaveAs(Server.MapPath("/UploadFiles/" + fileName));

                    string mainConn = ConfigurationManager.ConnectionStrings["RecordContext"].ConnectionString;
                    SqlConnection sqlconn = new SqlConnection(mainConn);
                    string sqlquery = "insert into [dbo].[FileFormat] values(@Firstname, @Lastname, @Name, @FilePath)";
                    SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                  
                    sqlconn.Open();
                    sqlcomm.Parameters.AddWithValue("@Firstname", Firstname);
                    sqlcomm.Parameters.AddWithValue("@Lastname", Lastname);
                    sqlcomm.Parameters.AddWithValue("@Name", fileName);
                    sqlcomm.Parameters.AddWithValue("@FilePath", "/UploadFiles/" + fileName);
                    sqlcomm.ExecuteNonQuery();
                    sqlconn.Close();
                    ViewData["Message"] = "Record Uploaded Successfully..!";
                }
                
            }
            return RedirectToAction("Index");
            
               
            }
    

        
        

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult ViewFiles(int Id)
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}