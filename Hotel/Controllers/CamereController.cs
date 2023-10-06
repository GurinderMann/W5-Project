using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CamereController : Controller
    {
        // GET: Camere
        public ActionResult Index()
        {

            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            List<Camere> camere = new List<Camere>();
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM T_CAMERA", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["IdCamera"]);
                    int nrCamera = Convert.ToInt32(reader["NrCamera"]);
                    string descrizione = reader["Descrizione"].ToString();
                    string tipo= reader["Tipo"].ToString();

                    Camere camera = new Camere
                    {
                        IdCamera = id,
                        NrCamera = nrCamera,
                        Descrizione = descrizione,
                        Tipo = tipo

                    };


                    camere.Add(camera);


                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            Camere.CamereList = camere;

            return View(Camere.CamereList);
        }


        //Creazione Camera 
        public ActionResult NuovoCamera()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuovoCamera(Camere c)
        {

            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);

            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO T_CAMERA (NrCamera, Descrizione, Tipo) " +
                                                "VALUES (@NrCamera, @Descrizione, @Tipo)", sqlConnection);

                cmd.Parameters.AddWithValue("@NrCamera", c.NrCamera);
                cmd.Parameters.AddWithValue("@Descrizione", c.Descrizione);
                cmd.Parameters.AddWithValue("@Tipo", c.Tipo);
         

                cmd.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally { sqlConnection.Close(); }
            return RedirectToAction("Index", "Camere");
        }


        //Modifica Camera

        public ActionResult ModificaCamera(int IdCamera)
        {
            Camere camera = new Camere();
            foreach (Camere c in Camere.CamereList)
            {
                if (c.IdCamera == IdCamera)
                {
                    camera = c; break;
                }
            }



            return View(camera);

        }

        [HttpPost]
        public ActionResult ModificaCamera(Camere c)
        {


            if (ModelState.IsValid)
            {

                string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(conn);

                try
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE  T_CAMERA SET NrCamera = @NrCamera, Descrizione = @Descrizione, Tipo = @Tipo WHERE IdCamera = @Id", sqlConnection);
                    cmd.Parameters.AddWithValue("@Id", c.IdCamera);
                    cmd.Parameters.AddWithValue("@NrCamera", c.NrCamera);
                    cmd.Parameters.AddWithValue("@Descrizione", c.Descrizione);
                    cmd.Parameters.AddWithValue("@Tipo", c.Tipo);
                                
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                finally { sqlConnection.Close(); }


            }
            return RedirectToAction("Index", "Camere");
        }
    }
}