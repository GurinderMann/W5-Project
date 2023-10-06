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
    public class ServiziController : Controller
    {
        // GET: Servizi
        public ActionResult Index( int IdS)
        {
            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            List<Servizi> servizi = new List<Servizi>();
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM T_SERVIZI WHERE FK_PRENOTAZIONI = @Id ", sqlConnection);
                cmd.Parameters.AddWithValue("@Id", IdS);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {   
                    int id = Convert.ToInt32(reader["IdServizio"]);
                    DateTime dataServizio = Convert.ToDateTime(reader["DataServizio"]);
                    string descrizione = reader["Descrizione"].ToString();
                    int quantita = Convert.ToInt32(reader["Quantità"]);
                    decimal prezzo = Convert.ToDecimal(reader["Prezzo"]);
                    int fkPrenotazione = Convert.ToInt32(reader["FK_Prenotazioni"]);

                    Servizi servizio = new Servizi 
                    {
                        IdServizio = id,
                        DataServizio = dataServizio,
                        Descrizione = descrizione,
                        Quantita = quantita,
                        Prezzo = prezzo,
                        FkPrenotazioni = fkPrenotazione
                    };

                    servizi.Add( servizio );

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

            Servizi.ServiziList = servizi;

            return View(Servizi.ServiziList);
        }

        //Aggiunta Servizi
        public ActionResult NuovoServizio()
        {
       
         return View();
        }

        [HttpPost]
        public ActionResult NuovoServizio(Servizi s)
        {
           
            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);

            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO T_SERVIZI (DataServizio, Descrizione, Quantità, Prezzo, FK_Prenotazioni) " +
                                                "VALUES (@DataServizio, @Descrizione, @Quantita, @Prezzo, @FkPrenotazioni)", sqlConnection);

                cmd.Parameters.AddWithValue("@DataServizio", s.DataServizio);
                cmd.Parameters.AddWithValue("@Descrizione", s.Descrizione);
                cmd.Parameters.AddWithValue("@Quantita", s.Quantita);
                cmd.Parameters.AddWithValue("@Prezzo", s.Prezzo);
                cmd.Parameters.AddWithValue("@FkPrenotazioni", s.FkPrenotazioni);
            
                cmd.ExecuteNonQuery();
                             

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally { sqlConnection.Close(); }
            return RedirectToAction("Index", "Home");
        }


        //Modifica Servizi

        public ActionResult ModificaServizio(int IdServizio)
        {
            Servizi servizio = new Servizi();
            foreach (Servizi s in Servizi.ServiziList)
            {
                if (s.IdServizio == IdServizio)
                {
                    servizio = s; break;
                }
            }



            return View(servizio);

        }

        [HttpPost]
        public ActionResult ModificaServizio(Servizi s)
        {


            if (ModelState.IsValid)
            {

                string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(conn);

                try
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE  T_SERVIZI SET DataServizio = @DataServizio, Descrizione = @Descrizione, Quantità = @Quantita, Prezzo = @Prezzo  WHERE IdServizio = @Id", sqlConnection);
                    cmd.Parameters.AddWithValue("@Id", s.IdServizio);
                    cmd.Parameters.AddWithValue("@DataServizio", s.DataServizio);
                    cmd.Parameters.AddWithValue("@Descrizione", s.Descrizione);
                    cmd.Parameters.AddWithValue("@Quantita", s.Quantita);
                    cmd.Parameters.AddWithValue("@Prezzo", s.Prezzo);
             

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                finally { sqlConnection.Close(); }


            }
            return RedirectToAction("Index", "Home");
        }
    }
}