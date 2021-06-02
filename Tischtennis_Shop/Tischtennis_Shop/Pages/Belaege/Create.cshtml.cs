using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tischtennis_Shop.Data;
using Tischtennis_Shop.Models;

namespace Tischtennis_Shop.Pages.Belaege
{
    public class CreateModel : PageModel
    {
        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public CreateModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string Mitarbeiderid { get; set; }

        [BindProperty(SupportsGet = true)]
        public string eingapreis { get; set; }


        public Mitarbeiter Mitarbeiter { get; set; }

        [BindProperty(SupportsGet = true)]
        public Belag Belag { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

      

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            // Hier wird geschaut ob alles der Valitierung entspricht wenn nicht wird die Seite erneut angezeigt
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // DEklaration Variable MitarbeiderID

            int MitarbeiderID = 0;

            // Abfangen des Falles eingabeid ist Null dann aufrufen der Seite FehlerMitarbeiternummer

            if (this.Mitarbeiderid == null)
            {
                return base.RedirectToPage("/FehlerMitarbeiternummer");
            }

            // Sonst MitarbeiderID ist = eingabeid
            else
            {
                MitarbeiderID = Convert.ToInt32(this.Mitarbeiderid);


            }

            // Suchen des Mitarbeiters mit der ID von Oben

            Mitarbeiter = _context.Mitarbeiter.FirstOrDefault(x => x.ID == MitarbeiderID);

            // Abfangen des Falles Mitarbeiter nict vorhanden
            if (Mitarbeiter == null)
            {
                return RedirectToPage("/FehlerMitarbeiternummer");
            }

            // Festlegen des Mitarbeiters der den Belag angelegt hat

            Belag.Mitarbeiter = Mitarbeiter;

            // Festlegen des Preises des Belages durch umwandeln der String eingabe

            Belag.Preis = Convert.ToDecimal(eingapreis);


            // Hinzufügen des Belages in der Datenbank

            _context.Belag.Add(Belag);

            // Speichern der Änderung

            await _context.SaveChangesAsync();

            // zurück zur Index Seite

            return RedirectToPage("./Index");
        }
    }
}
