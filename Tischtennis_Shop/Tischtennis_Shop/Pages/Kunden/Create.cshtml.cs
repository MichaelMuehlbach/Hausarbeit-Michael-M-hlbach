using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tischtennis_Shop.Data;
using Tischtennis_Shop.Models;

namespace Tischtennis_Shop.Pages.Kunden
{
    public class CreateModel : PageModel
    {
        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public CreateModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public Kunde Kunde { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            // überprüfung der Valedierung
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // erstellen Einer neuen Rechnung
            Rechnung r1 = new Rechnung();
            // Aussuchen der Verkauften Waren die noch keiner Rechnung zugeortnit sind
            var Waremenge =   from w in _context.Verkaufte_Ware where w.Rechnung == null select w ;
            // Berechnung des Gesamtbetrages der rechnung
            foreach (Verkaufte_Ware w in Waremenge)
            {
                r1.Gesamtbetrag = r1.Gesamtbetrag + ( w.Menge * w.Preis_je_Stueck);
            }
            // Hinzufügen des Kunden in der Datenbank
            _context.Kunde.Add(Kunde);
            // Kunde Rechnung zuweisen
            r1.Kunde = Kunde;
            // Hinzufügen der Rechnung in der Datenbank
            _context.Rechnung.Add(r1);
            // Verkaufte Ware Rechnung zuweisen
            foreach (Verkaufte_Ware w in Waremenge)
            {
                w.Rechnung = r1;
            }
            // Speichern der änderungen
            await _context.SaveChangesAsync();
            // weiter zur Seite Rechnung_anzeigen
            return RedirectToPage("/Rechnung_anzeigen");
        }
    }
}
