using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tischtennis_Shop.Models;


namespace Tischtennis_Shop.Pages
{
    public class BelaegeKaufenModel : PageModel
    {

        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public BelaegeKaufenModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }



        [BindProperty(SupportsGet = true)]
        public int Menge { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Farbe { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Marke { get; set; }


        public Belag Belag { get; set; }




       

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Belag = await _context.Belag.FirstOrDefaultAsync(m => m.ID == id);

            Name = Belag.Name;

            if (Belag == null)
            {
                return NotFound();
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {

            int Mengegekauft = Menge;

            Belag Gekaufterbelag = new Belag();
            var b1 = _context.Belag.Where(x => x.Name == Name);

            foreach (Belag b in b1) 
            {
                Gekaufterbelag = b;
            }


            Verkaufte_Ware vw1 = new Verkaufte_Ware();

            if(Farbe == null) 
            {
                return RedirectToPage("./FehlerKaufen");

            }

            if (Menge == 0)
            {
                return RedirectToPage("./FehlerKaufen");
            }

            if (Farbe == "Rot" && ( Gekaufterbelag.Menge_Rot - Menge ) >= 0  ) 
            {
                vw1.Menge =  Mengegekauft;

                Gekaufterbelag.Menge_Rot = (Gekaufterbelag.Menge_Rot - Menge);
            }

          


            if (Farbe == "Schwarz" && ( Gekaufterbelag.Menge_Schwarz - Menge ) >= 0) 
            {
                vw1.Menge = Mengegekauft;
                Gekaufterbelag.Menge_Schwarz = ( Gekaufterbelag.Menge_Schwarz - Menge );
            
            }


           

            vw1.Name = Gekaufterbelag.Marke + ", " + Gekaufterbelag.Name;  

           if (Menge >= 5) 
            {
                vw1.Preis_je_Stueck = Gekaufterbelag.Preis - 3;
                vw1.Gesamtpreis = vw1.Preis_je_Stueck * Menge;
            }

           if (Menge < 5)    
            {
                vw1.Preis_je_Stueck = Gekaufterbelag.Preis;
                vw1.Gesamtpreis = vw1.Preis_je_Stueck * Menge;

            }
          

            vw1.Farbe = Farbe;

            vw1.Belag = Gekaufterbelag;

            _context.Verkaufte_Ware.Add(vw1);

            await _context.SaveChangesAsync();

           


            return  RedirectToPage("./Index");
        }



    }
}
