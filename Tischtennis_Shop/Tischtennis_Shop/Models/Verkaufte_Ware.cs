using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tischtennis_Shop.Models
{
    public class Verkaufte_Ware
    {
        public int ID { get; set; }

        public string Name { get; set; }


        public string Farbe { get; set; }


        public int Menge { get; set; }

        public decimal Preis_je_Stueck { get; set; }


        public decimal Gesamtpreis { get; set; }

        public Rechnung Rechnung { get; set; }

        public Belag Belag { get; set; }


    }
}
