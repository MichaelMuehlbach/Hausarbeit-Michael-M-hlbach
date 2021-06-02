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
        // Validierung Hinzugefügt am Anfang muss ein groß Bustabe sein und danach klein Bustaben
        [RegularExpression(@"^[A-Z]+[a-z\s]*$")]
        [Required(ErrorMessage = "Bitte geben sie ihren Vornamen ein")]
        [StringLength(20)]
        [BindProperty(SupportsGet = true)]
        public string Vorname { get; set; }

        // Validierung Hinzugefügt am Anfang muss ein groß Bustabe sein und danach klein Bustaben
        [RegularExpression(@"^[A-Z]+[a-z\s]*$")]
        [Required(ErrorMessage = "Bitte geben sie ihren Nachnamen ein.")]
        [StringLength(20)]
        [BindProperty(SupportsGet = true)]
        public string Nachname { get; set; }

        public Kunde Kundeakttuel { protected get; set; }

        [Required(ErrorMessage = "Bitte geben sie ihre Kundennummer ein")]
        [BindProperty(SupportsGet = true)]
        public int ideingabe { get; set; }



        public async Task<IActionResult> OnPostAsync()
        {
            // Konntrolle der Valedierung
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Alle Kunden aus der Datenbank abfragen
            var Kundemenge = from m in _context.Kunde select m;
            // Herraussuchen des Kunen anhand der Eingaben
            Kundeakttuel = Kundemenge.FirstOrDefault(x => x.Nachname == Nachname && x.Vorname == Vorname && x.ID == ideingabe);
            // Abfangen Kunde = null
            if (Kundeakttuel != null)
            {
                // Ertellen einer neuen Rechnung
                Rechnung r1 = new Rechnung();
                // Aussuchen der Verkauften Waren die noch keiner Rechnung zugeortnit sind
                var Waremenge = from w in _context.Verkaufte_Ware where w.Rechnung == null select w;
                // Berechnung des Gesamtbetrages der rechnung
                foreach (Verkaufte_Ware w in Waremenge)
                {
                    r1.Gesamtbetrag = r1.Gesamtbetrag + (w.Menge * w.Preis_je_Stueck);
                }
                // Kunde Rechnung zuweisen
                r1.Kunde = Kundeakttuel;
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
            else
            {
                // weiter zur Seite Falsche_Angabe
                return RedirectToPage("/Falsche_Angabe");
            }
        }
    }
}
