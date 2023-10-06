using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Camere
    {
        public int IdCamera { get; set; }
        public int NrCamera { get; set; }   
        public string Descrizione { get; set; }
        public string Tipo { get; set; }

        public int Value { get; set; }
        public string Text { get; set; }
        
        public static List<Camere> CamereList { get; set;} = new List<Camere>();
    }
}