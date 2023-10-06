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
    public class AsyncController : Controller
    {
        // GET: Async
        public ActionResult Index()
        {
            return View();
        }


        //Prenotazioni in base al codice fiscale
        public JsonResult PrenotazioniCliente(string codiceFiscale)
        {
            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            List<Prenotazioni> prenotazioniCliente = new List<Prenotazioni>();

            try
            {
                sqlConnection.Open();

                string query = "SELECT P.*, C.Nome AS NomeCliente " +
                               "FROM T_Prenotazioni P " +
                               "INNER JOIN T_Clienti C ON P.FK_Clienti = C.IdCliente " +
                               "WHERE C.CF = @CodiceFiscale";

                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@CodiceFiscale", codiceFiscale);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Leggi i dati della prenotazione
                    int id = Convert.ToInt32(reader["IdPrenotazione"]);
                    DateTime dataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]);
                    int anno = Convert.ToInt32(reader["Anno"]);
                    DateTime dataArrivo = Convert.ToDateTime(reader["DataArrivo"]);
                    DateTime dataPartenza = Convert.ToDateTime(reader["DataPartenza"]);
                    decimal caparra = Convert.ToDecimal(reader["Caparra"]);
                    decimal tariffa = Convert.ToDecimal(reader["Tariffa"]);
                    string pensione = reader["Pensione"].ToString();
                    string nomeCliente = reader["NomeCliente"].ToString();

                    // Formatta le date come stringhe "dd/MM/yyyy"
                    string dataPrenotazioneStr = dataPrenotazione.ToString("dd/MM/yyyy");
                    string dataArrivoStr = dataArrivo.ToString("dd/MM/yyyy");
                    string dataPartenzaStr = dataPartenza.ToString("dd/MM/yyyy");

                    Prenotazioni prenotazione = new Prenotazioni
                    {
                        IdPrenotazione = id,
                        DataP = dataPrenotazioneStr,
                        Anno = anno,
                        DataA = dataArrivoStr,
                        DataPr = dataPartenzaStr,
                        Caparra = caparra,
                        Tariffa = tariffa,
                        Pensione = pensione,
                    };

                    prenotazioniCliente.Add(prenotazione);
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

            return Json(prenotazioniCliente, JsonRequestBehavior.AllowGet);
        }


        //Prenotazioni in base alla pensione completa
        public JsonResult PensioneCompleta()
        {
            string pension = "Pensione completa";
            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            List<Prenotazioni> prenotazioniCliente = new List<Prenotazioni>();

            try
            {
                sqlConnection.Open();

                string query = "SELECT * FROM T_PRENOTAZIONI WHERE Pensione=@Pensione";

                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@Pensione", pension);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                   
                    int id = Convert.ToInt32(reader["IdPrenotazione"]);
                    DateTime dataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]);
                    int anno = Convert.ToInt32(reader["Anno"]);
                    DateTime dataArrivo = Convert.ToDateTime(reader["DataArrivo"]);
                    DateTime dataPartenza = Convert.ToDateTime(reader["DataPartenza"]);
                    decimal caparra = Convert.ToDecimal(reader["Caparra"]);
                    decimal tariffa = Convert.ToDecimal(reader["Tariffa"]);
                    string pensione = reader["Pensione"].ToString();
                   

                    
                    string dataPrenotazioneStr = dataPrenotazione.ToString("dd/MM/yyyy");
                    string dataArrivoStr = dataArrivo.ToString("dd/MM/yyyy");
                    string dataPartenzaStr = dataPartenza.ToString("dd/MM/yyyy");

                    Prenotazioni prenotazione = new Prenotazioni
                    {
                        IdPrenotazione = id,
                        DataP = dataPrenotazioneStr,
                        Anno = anno,
                        DataA = dataArrivoStr,
                        DataPr = dataPartenzaStr,
                        Caparra = caparra,
                        Tariffa = tariffa,
                        Pensione = pensione,
                    };

                    prenotazioniCliente.Add(prenotazione);
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

            return Json(prenotazioniCliente, JsonRequestBehavior.AllowGet);
        }



    }
}