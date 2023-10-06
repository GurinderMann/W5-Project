using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Clienti
    {
        public int IdCliente { get; set; }
        public  string CF { get; set; }
        public  string Nome { get; set; }
        public string Cognome { get; set; }
        public string Citta { get; set; }
        public string Provincia { get; set; }
        public string Email { get; set; }
        public string Cellulare { get; set; }

        public int Value { get; set; }
        public string Text { get; set; }


        public static List<Clienti> ListClienti { get; set;} = new List<Clienti>();
    }
}