using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tischtennis_Shop.Models;


namespace Tischtennis_Shop.Pages
{
    public class Rechnung_anzeigenModel : PageModel
    {

        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public Rechnung_anzeigenModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

           public IList<Verkaufte_Ware> Ware { get; set; }

            public Rechnung Rechnung { get;set; }

        public Kunde Kunde { get; set; }

        public void OnGet()
        {
            // Alle rechnungen aus der Datenbank rausgeschrieben
            var Rechnungen = from R in _context.Rechnung select R ;
            // Rechnung besetzen mit der letzen Rechnung durch das man alle durchgeht und nur die letzte gibt dann ihren werd an Rechnung weiter
            foreach (Rechnung r in Rechnungen)
            {
                Rechnung = r;
            }           
            // Alle kunden rausschreiben aus der Datenbank
            var Kunden = from K in _context.Kunde  select K;
            // Waren aus der Datenabank schreiben die die rechnungs Id haben
            var Waren = from W in _context.Verkaufte_Ware where W.Rechnung == Rechnung select W;
            // Kunden Suchen der die rechnung bestellt hat
            foreach (Kunde k in Kunden) 
            {
                if(Rechnung.Kunde == k) 
                {
                    Kunde = k;
                }
            }
            // Hinzufügen der Waren zur IList Ware
            Ware = Waren.ToList();
        }
    }
}
