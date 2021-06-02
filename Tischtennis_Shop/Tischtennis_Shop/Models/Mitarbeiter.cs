using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tischtennis_Shop.Models
{
    public class Mitarbeiter
    {
        public int ID { get; set; }

        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public string Salt { get; set; }

        public string Passwort { get; set; }


        public ICollection<Belag> Belag { get; set; }





    }
}
