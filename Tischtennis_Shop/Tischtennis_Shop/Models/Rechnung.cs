using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tischtennis_Shop.Models
{
    public class Rechnung
    {

        public int ID { get; set; }

        public decimal Gesamtbetrag { get; set; }


        public Kunde Kunde { get; set; }

        public ICollection<Verkaufte_Ware> Verkaufte_Ware { get; set; }
       

    }
}
