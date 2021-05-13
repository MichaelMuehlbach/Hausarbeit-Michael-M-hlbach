using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tischtennis_Shop.Models;


namespace Tischtennis_Shop.Pages
{
    public class WarenkorbModel : PageModel
    {

        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;
  
        public WarenkorbModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }


        public IList<Verkaufte_Ware> Ware { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal Zwischensumme { get; set; }

     

        public void OnGet()
        {

            var Waremenge = from w in _context.Verkaufte_Ware where w.Rechnung == null select w;


            foreach( Verkaufte_Ware vw in Waremenge) 
            {
                Zwischensumme = Zwischensumme + vw.Gesamtpreis;
            }

            Ware = Waremenge.ToList();



        }
    }
}
