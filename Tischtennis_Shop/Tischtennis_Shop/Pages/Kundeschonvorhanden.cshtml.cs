using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tischtennis_Shop.Models;

namespace Tischtennis_Shop.Pages
{
    public class KundeschonvorhandenModel : PageModel
    {

        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public KundeschonvorhandenModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

        [RegularExpression(@"^[A-Z]+[a-z\s]*$"), Required, StringLength(20)]
        [BindProperty(SupportsGet = true)]
        public string Vorname { get; set; }


        [RegularExpression(@"^[A-Z]+[a-z\s]*$"), Required, StringLength(20)]
        [BindProperty(SupportsGet = true)]
        public string Nachname { get; set; }

        public Kunde Kundeakttuel { protected get; set; }

        [BindProperty(SupportsGet = true)]
        public int ideingabe { get; set; }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var Kundemenge = from m in _context.Kunde select m;

        


            Kundeakttuel = Kundemenge.FirstOrDefault(x => x.Nachname == Nachname && x.Vorname == Vorname && x.ID == ideingabe);




            if (Kundeakttuel != null)
            {

                Rechnung r1 = new Rechnung();

                var Waremenge = from w in _context.Verkaufte_Ware where w.Rechnung == null select w;



                foreach (Verkaufte_Ware w in Waremenge)
                {
                    r1.Gesamtbetrag = r1.Gesamtbetrag + (w.Menge * w.Preis_je_Stueck);


                }


                r1.Kunde = Kundeakttuel;

                _context.Rechnung.Add(r1);

                foreach (Verkaufte_Ware w in Waremenge)
                {
                    w.Rechnung = r1;


                }





                await _context.SaveChangesAsync();

               


             return RedirectToPage("/Rechnung_anzeigen");





            }

            else
            {
                return RedirectToPage("/Falsche_Angabe");
            }

        }
    }
}
