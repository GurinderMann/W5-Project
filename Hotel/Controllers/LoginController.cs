using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Hotel.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Users u)
        {
            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("Select * FROM T_DIPENDENTI WHERE Username=@Username And Password=@Password", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Username", u.Username);
                sqlCommand.Parameters.AddWithValue("Password", u.Password);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.AuthError = "Autenticazione non riuscita";
                    return View();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());

            }
            finally
            {
                sqlConnection.Close();
            }

            return RedirectToAction("Index", "Home");
        }


        //Codice per registrarsi
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users u)
        {
            string role = "Admin";
            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO T_Dipendenti (Username, Password, Role) VALUES(@Username, @Password, @Role)", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Username", u.Username);
                sqlCommand.Parameters.AddWithValue("@Password", u.Password);
                sqlCommand.Parameters.AddWithValue("@Role", role);

                sqlCommand.ExecuteReader();

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());

            }
            finally
            {
                sqlConnection.Close();
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");

        }
    }
}