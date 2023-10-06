using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Prenotazioni
    {
        public int IdPrenotazione { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public int Anno { get; set; }
        public DateTime DataArrivo { get; set; }
        public DateTime DataPartenza { get; set; }
        public decimal Caparra { get; set; }
        public decimal Tariffa { get; set; }
        public string Pensione { get; set; }
        public int FKClienti { get; set; }
        public int FkCamere { get; set; }

        public string DataP { get; set; }
        public string DataA { get; set; }
        public string DataPr { get; set; }
        public static List<Prenotazioni> ListaP { get; set; } = new List<Prenotazioni>();
    }
}