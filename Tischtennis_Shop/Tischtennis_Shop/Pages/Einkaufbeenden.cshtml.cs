using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tischtennis_Shop.Models;

namespace Tischtennis_Shop.Pages
{
    public class EinkaufbeendenModel : PageModel
    {
        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public EinkaufbeendenModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public decimal Zwischensumme { get; set; }

        public IList<Verkaufte_Ware> Ware { get; set; }

        public void OnGet()
        {
            int Anzahl = 0;
            var Waremenge = from w in _context.Verkaufte_Ware where w.Rechnung == null select w;

           foreach (Verkaufte_Ware w in Waremenge) 
            {
                Anzahl = Anzahl + 1;
            }

            
            if (Anzahl == 0) 
            {
                Response.Redirect("/Warenkorbleer_BeendenEinkauf");
            }


            foreach (Verkaufte_Ware vw in Waremenge)
            {
                Zwischensumme = Zwischensumme + vw.Gesamtpreis;
            }

            Ware = Waremenge.ToList();
        }
    }
}
