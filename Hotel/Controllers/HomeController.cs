using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
     
        //Lista prenotazioni
        public ActionResult Index()
        {
            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            List<Prenotazioni> prenotazioni = new List<Prenotazioni>();
            try 
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM T_Prenotazioni ", sqlConnection);
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
                   
                    Prenotazioni prenotazione = new Prenotazioni
                    {
                        IdPrenotazione = id,
                        DataPrenotazione = dataPrenotazione,
                        Anno = anno,
                        DataArrivo = dataArrivo,
                        DataPartenza = dataPartenza,
                        Caparra = caparra,
                        Tariffa = tariffa,
                        Pensione = pensione

                    };

                    prenotazioni.Add(prenotazione);

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

            Prenotazioni.ListaP = prenotazioni;
            return View(Prenotazioni.ListaP);
        }


        //Creazione Prenotazione 
        public ActionResult NuovaPrenotazione()
        {
            ViewBag.camere = new List<Camere>();
            ViewBag.clienti = new List<Clienti>();
            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM T_CLIENTI", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    ViewBag.clienti.Add(new Clienti()
                    {
                        Value = Convert.ToInt32(reader["IdCliente"].ToString()),
                        Text = reader["Nome"].ToString(),
                    });

            }
            catch { }
            finally { sqlConnection.Close(); }
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM T_CAMERA", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    ViewBag.camere.Add(new Camere()
                    {
                        Value = Convert.ToInt32(reader["IdCamera"].ToString()),
                        Text = reader["NrCamera"].ToString(),
                    });

            }
            catch { }
            finally { sqlConnection.Close(); }
            return View();
        }

        [HttpPost]
        public ActionResult NuovaPrenotazione(Prenotazioni p)
        {
            ViewBag.camere = new List<Camere>();
            ViewBag.clienti = new List<Clienti>();
            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);

            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO T_PRENOTAZIONI (DataPrenotazione, Anno, DataArrivo, DataPartenza, Caparra, Tariffa, Pensione, FK_Clienti, FK_Camere) " +
                                                "VALUES (@DataPrenotazione, @Anno, @DataArrivo, @DataPartenza, @Caparra, @Tariffa, @Pensione, @FKCliente, @FKCamere)", sqlConnection);

                cmd.Parameters.AddWithValue("@DataPrenotazione", p.DataPrenotazione);
                cmd.Parameters.AddWithValue("@Anno", p.Anno);
                cmd.Parameters.AddWithValue("@DataArrivo", p.DataArrivo);
                cmd.Parameters.AddWithValue("@DataPartenza", p.DataPartenza);
                cmd.Parameters.AddWithValue("@Caparra", p.Caparra);
                cmd.Parameters.AddWithValue("@Tariffa", p.Tariffa);
                cmd.Parameters.AddWithValue("@Pensione", p.Pensione);
                cmd.Parameters.AddWithValue("@FKCliente", p.FKClienti);
                cmd.Parameters.AddWithValue("@FKCamere", p.FkCamere);

                cmd.ExecuteNonQuery();




                cmd = new SqlCommand("SELECT * FROM T_CLIENTI", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    ViewBag.clienti.Add(new Clienti()
                    {
                        Value = Convert.ToInt32(reader["IdCliente"].ToString()),
                        Text = reader["Nome"].ToString(),
                    });

                cmd = new SqlCommand("SELECT * FROM T_CAMERA", sqlConnection);
                reader.Close();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    ViewBag.camere.Add(new Camere()
                    {
                        Value = Convert.ToInt32(reader["IdCamera"].ToString()),
                        Text = reader["NrCamera"].ToString(),
                    });


            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally { sqlConnection.Close(); }
            return View();
        }


        //Modifica Prenotazione
        public ActionResult ModificaPrenotazione(int IdPrenotazione)
        {
            Prenotazioni prenotazione = new Prenotazioni();
            foreach (Prenotazioni p in Prenotazioni.ListaP)
            {
                if (p.IdPrenotazione == IdPrenotazione)
                {
                   prenotazione = p; break;
                }
            }
           
            ViewBag.camere = new List<Camere>();
            ViewBag.clienti = new List<Clienti>();

            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM T_CLIENTI", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    ViewBag.clienti.Add(new Clienti()
                    {
                        Value = Convert.ToInt32(reader["IdCliente"].ToString()),
                        Text = reader["Nome"].ToString(),
                    });

            }
            catch { }
            finally { sqlConnection.Close(); }
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM T_CAMERA", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    ViewBag.camere.Add(new Camere()
                    {
                        Value = Convert.ToInt32(reader["IdCamera"].ToString()),
                        Text = reader["NrCamera"].ToString(),
                    });

            }
            catch { }
            finally { sqlConnection.Close(); }
            return View(prenotazione);

        }

        [HttpPost]
        public ActionResult ModificaPrenotazione(Prenotazioni p)
        {
            ViewBag.camere = new List<Camere>();
            ViewBag.clienti = new List<Clienti>();

            if (ModelState.IsValid)
            {
                
                string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(conn);
             
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE  T_PRENOTAZIONI SET " +
                                                    "DataPrenotazione = @DataPrenotazione, Anno = @Anno, DataArrivo = @DataArrivo, " +
                                                    "DataPartenza = @DataPartenza, Caparra = @Caparra, Tariffa=@Tariffa, Pensione =@Pensione," +
                                                    "FK_Clienti = @FkClienti, FK_Camere = @FK_Camere WHERE IdPrenotazione = @Id  ", sqlConnection);
                    cmd.Parameters.AddWithValue("@Id", p.IdPrenotazione);
                    cmd.Parameters.AddWithValue("@DataPrenotazione", p.DataPrenotazione);
                    cmd.Parameters.AddWithValue("@Anno", p.Anno);
                    cmd.Parameters.AddWithValue("@DataArrivo", p.DataArrivo);
                    cmd.Parameters.AddWithValue("@DataPartenza", p.DataPartenza);
                    cmd.Parameters.AddWithValue("@Caparra", p.Caparra);
                    cmd.Parameters.AddWithValue("@Tariffa", p.Tariffa);
                    cmd.Parameters.AddWithValue("@Pensione", p.Pensione);
                    cmd.Parameters.AddWithValue("@FKClienti", p.FKClienti);
                    cmd.Parameters.AddWithValue("@FK_Camere", p.FkCamere);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT * FROM T_CLIENTI", sqlConnection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                        ViewBag.clienti.Add(new Clienti()
                        {
                            Value = Convert.ToInt32(reader["IdCliente"].ToString()),
                            Text = reader["Nome"].ToString(),
                        });


                    cmd = new SqlCommand("SELECT * FROM T_CAMERA", sqlConnection);
                   
                    reader.Close();

                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                        ViewBag.camere.Add(new Camere()
                        {
                            Value = Convert.ToInt32(reader["IdCamera"].ToString()),
                            Text = reader["NrCamera"].ToString(),
                        });


                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                finally { sqlConnection.Close(); }


            }
            return RedirectToAction("Index", "Home");
        }



        //Dettagli Prenotazione 

        public ActionResult DettagliPrenotazione(int IdPrenotazione)
        {
            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            List<Servizi> servizi = new List<Servizi>();
            try
            {
                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("GetPrenotazioneDetails", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue ("@IdPrenotazione", IdPrenotazione);


                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int numeroStanza = Convert.ToInt32(reader["NumeroStanza"]);
                        DateTime dataArrivo = Convert.ToDateTime(reader["DataArrivo"]);
                        DateTime dataPartenza = Convert.ToDateTime(reader["DataPartenza"]);
                        decimal tariffaApplicata = Convert.ToDecimal(reader["TariffaApplicata"]);
                        decimal caparra = Convert.ToDecimal(reader["Caparra"]);
                        decimal sommaServiziAggiuntivi = Convert.ToDecimal(reader["SommaServiziAggiuntivi"]);
                        decimal importoDaSaldare = Convert.ToDecimal(reader["ImportoDaSaldare"]);

                        ViewBag.NumeroStanza = numeroStanza;
                        ViewBag.DataArrivo = dataArrivo.ToString("dd/MM/yyyy");
                        ViewBag.DataPartenza = dataPartenza.ToString("dd/MM/yyyy");
                        ViewBag.TariffaApplicata = tariffaApplicata;
                        ViewBag.Caparra = caparra;
                        ViewBag.SommaServiziAggiuntivi = sommaServiziAggiuntivi;
                        ViewBag.ImportoDaSaldare = importoDaSaldare;

                    
                    }
                }
                else
                {
                 
                    return View("ErrorePrenotazioneNonTrovata");
                }

                cmd = new SqlCommand("SELECT * FROM T_SERVIZI WHERE FK_PRENOTAZIONI = @id", sqlConnection);
                cmd.Parameters.AddWithValue("@id", IdPrenotazione);
                reader.Close();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime dataServizio = Convert.ToDateTime(reader["DataServizio"]);
                    string descrizione = reader["Descrizione"].ToString();
                    int quantita = Convert.ToInt32(reader["Quantità"]);
                    decimal prezzo = Convert.ToDecimal(reader["Prezzo"]);

                    Servizi servizio = new Servizi 
                    {
                        DataServizio = dataServizio,
                        Descrizione = descrizione,
                        Quantita = quantita,
                        Prezzo = prezzo

                    };
                    servizi.Add(servizio);
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
             return View(servizi);
        }   







    }
}