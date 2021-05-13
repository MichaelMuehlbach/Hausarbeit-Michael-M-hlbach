using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tischtennis_Shop.Models
{
    public class Belag
    {
        public int ID { get; set; }

        public string Belag_Art { get; set; }

        public int Menge_Schwarz { get; set; }

        public int Menge_Rot { get; set; }

        public string Marke { get; set; }

        public string Name { get; set; }

        public decimal Preis { get; set; }

        public string Bildpfad { get; set; }


        public Mitarbeiter Mitarbeiter { get; set; }


       





    }
}
