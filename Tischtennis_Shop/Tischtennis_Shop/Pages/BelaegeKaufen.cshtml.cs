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
            // Abfangen des Falles ID ist = null
            if (id == null)
            {
                return NotFound();
            }       
            // Heraussuchen des Belages mit der Passenden ID                
            Belag = await _context.Belag.FirstOrDefaultAsync(m => m.ID == id);            
            // Namen des Belages in die Varaiable Name schreiben 
            Name = Belag.Name;
            // Abfangen fall Belag = null
            if (Belag == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // Mengegekauft variable mit der Variable Menge besetzen 
            
            int Mengegekauft = Menge;

            // Deklariereen einer neuen Belag Varaible

            Belag Gekaufterbelag = new Belag();

            // Suchen desm Passenden Belages anhand des Namens

            var b1 = _context.Belag.Where(x => x.Name == Name);

            // Gekaufterbaleg belegen mit dem Gefundene Belag

            foreach (Belag b in b1) 
            {
                Gekaufterbelag = b;
            }

            // Deklarieren einer neuen Verkauften Ware

            Verkaufte_Ware vw1 = new Verkaufte_Ware();

            // Abfangen Fall Farbe = null

            if(Farbe == null) 
            {
                return RedirectToPage("./FehlerKaufen");
            }

            // Abfangen Fall Menge = null

            if (Menge == 0)
            {
                return RedirectToPage("./FehlerKaufen");
            }

            // Abfangen Fall zuviel gekauft bei Farbe Rot

            if (Farbe == "Rot" && ( Gekaufterbelag.Menge_Rot - Menge ) >= 0  ) 
            {
                vw1.Menge =  Mengegekauft;
                Gekaufterbelag.Menge_Rot = (Gekaufterbelag.Menge_Rot - Menge);
            }
            // Abfangen Fall zuviel gekauft bei Farbe Schwarz

            if (Farbe == "Schwarz" && ( Gekaufterbelag.Menge_Schwarz - Menge ) >= 0) 
            {
                vw1.Menge = Mengegekauft;
                Gekaufterbelag.Menge_Schwarz = ( Gekaufterbelag.Menge_Schwarz - Menge );
            }

           // verkaufte Ware Namen zusammensetzung aus Marke + Name des Belages

            vw1.Name = Gekaufterbelag.Marke + ", " + Gekaufterbelag.Name;
            
            // Mengenrabat konntrolle und danach anpassen des Preises

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

           // Verkaufte Ware Farbe festlegen

            vw1.Farbe = Farbe;

            // Varkaufte Ware Belag festlegen

            vw1.Belag = Gekaufterbelag;

            // Hinzufügen der Verkauften Ware

            _context.Verkaufte_Ware.Add(vw1);

            // Speichern der Änderungen

            await _context.SaveChangesAsync();

            // zurück zur Seite Index

            return  RedirectToPage("./Index");
        }  
    }
}
