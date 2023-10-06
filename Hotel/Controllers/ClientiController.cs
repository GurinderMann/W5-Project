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
    public class ClientiController : Controller
    {
        // GET: Clienti
        public ActionResult Index()
        {

            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);
            List<Clienti> clienti = new List<Clienti>();
            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM T_CLIENTI ", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["IdCliente"]);
                    string cf = reader["CF"].ToString();
                    string cognome = reader["Cognome"].ToString();
                    string nome = reader["Nome"].ToString();
                    string citta = reader["Città"].ToString();
                    string provincia = reader["Provincia"].ToString();
                    string email = reader["Email"].ToString();
                    string cellulare = reader["Cellulare"].ToString();

                    Clienti cliente = new Clienti 
                    {
                        IdCliente = id,
                        CF = cf,
                        Cognome = cognome,
                        Nome = nome,
                        Citta = citta,
                        Provincia = provincia,
                        Email = email,
                        Cellulare = cellulare
                    };

                    clienti.Add(cliente);


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

            Clienti.ListClienti = clienti;
           
            return View(Clienti.ListClienti);
        }


        //Creazione  Cliente
        public ActionResult NuovoCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuovoCliente(Clienti c)
        {
            
            string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);

            try
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO T_CLIENTI (CF, Cognome, Nome, Città, Provincia, Email, Cellulare) " +
                                                "VALUES (@CF, @Cognome,@Nome, @Citta, @Provincia, @Email, @Cellulare)", sqlConnection);

                cmd.Parameters.AddWithValue("@CF", c.CF);
                cmd.Parameters.AddWithValue("@Cognome", c.Cognome);
                cmd.Parameters.AddWithValue("@Nome", c.Nome);
                cmd.Parameters.AddWithValue("@Citta", c.Citta);
                cmd.Parameters.AddWithValue("@Provincia", c.Provincia);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Cellulare", c.Cellulare);

              
                cmd.ExecuteNonQuery();
                             


            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally { sqlConnection.Close(); }
            return View();
        }


        //Modifica Cliente
        public ActionResult ModificaCliente(int IdCliente)
        {
            Clienti cliente = new Clienti();
            foreach (Clienti c in Clienti.ListClienti)
            {
                if ( c.IdCliente == IdCliente)
                {
                    cliente = c; break;
                }
            }

          

            return View(cliente);

        }

        [HttpPost]
        public ActionResult ModificaCliente(Clienti c)
        {
    

            if (ModelState.IsValid)
            {

                string conn = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(conn);
               
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE  T_CLIENTI SET CF=@cf, Nome=@nome, Cognome=@cognome, Città=@citta, Provincia=@provincia, Email=@email, Cellulare=@cellulare WHERE IdCliente = @Id  ", sqlConnection);
                    cmd.Parameters.AddWithValue("@Id", c.IdCliente);
                    cmd.Parameters.AddWithValue("@cf", c.CF);
                    cmd.Parameters.AddWithValue("@nome", c.Nome);
                    cmd.Parameters.AddWithValue("@cognome", c.Cognome);
                    cmd.Parameters.AddWithValue("@citta", c.Citta);
                    cmd.Parameters.AddWithValue("@provincia", c.Provincia);
                    cmd.Parameters.AddWithValue("@email", c.Email);
                    cmd.Parameters.AddWithValue("@cellulare", c.Cellulare);


                   
                    cmd.ExecuteNonQuery();

                  


                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                finally { sqlConnection.Close(); }


            }
            return RedirectToAction("Index", "Clienti");
        }


    }
}