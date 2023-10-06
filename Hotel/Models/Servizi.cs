using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Servizi
    {
        public int IdServizio { get; set; }
        public DateTime DataServizio { get; set; }
        public string Descrizione { get; set; }
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }
        public int FkPrenotazioni { get; set; }

        public static List<Servizi> ServiziList { get; set; } = new List<Servizi>();
    }
}