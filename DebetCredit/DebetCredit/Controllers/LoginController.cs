using System;
using System.Data;
using System.Security;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DebetCredit.Models;
using System.Web.Security;

namespace DebetCredit.Controllers
{
    public class LoginController : Controller
    {
        string connectionString = @"Data Source = ACER\SQLEXPRESS; Initial Catalog = DebetCreditdb; Integrated Security=True";
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(LoginModel lc)
        {
            SqlConnection sqlconn = new SqlConnection(connectionString);
            string sqlquery = "select Login, Pass from Table_3 WHERE Login=@Login and Pass=@Pass";
            sqlconn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@Login", lc.Login);
            sqlcomm.Parameters.AddWithValue("@Pass", lc.Pass);
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            if (sdr.Read())
            {
                Session["ID"] = lc.ID.ToString();
                return RedirectToAction("Index", "Balance");
            }
            else
            { 
                sqlconn.Close();
                return View("Index");
                //ViewData["Message"] = "Введен неправильный логин или пароль!";
            }
           
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Login");
        }


    }
}