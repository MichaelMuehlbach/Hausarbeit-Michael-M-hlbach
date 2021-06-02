using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tischtennis_Shop.Data;
using Tischtennis_Shop.Models;

namespace Tischtennis_Shop.Pages.Belaege
{
    public class DeleteModel : PageModel
    {
        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public DeleteModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Belag Belag { get; set; }

        public int MitarbeiterID { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            // Abfangen des Falles Belag ist nicht vorhanden

            if (id == null)
            {
                return NotFound();
            }

            // Suchen des Belages

            Belag = await _context.Belag.FirstOrDefaultAsync(m => m.ID == id);


            // Suchenn des Mitarbeiters der die Lätzte Änderung vorgenommen hat

            var Mitarbeiermenge = from m in _context.Belag where m.ID == id select m.Mitarbeiter.ID;

            foreach (int i in Mitarbeiermenge)
            {

                MitarbeiterID = i;
            }


            // Abfangen des Falles Belag nicht vorhanden 

            if (Belag == null)
            {
                return NotFound();
            }

            // Rückgabe der Seite
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {

            // Abfangen des Falles ID ist nicht vorhanden

            if (id == null)
            {
                return NotFound();
            }

            // Suchen des Belages

            Belag = await _context.Belag.FindAsync(id);
            // Abfangen des Falles Belag nicht vorhanden 

            if (Belag != null)
            {

                // Löschen des Belages
                _context.Belag.Remove(Belag);

                // Änderungen Speichern

                await _context.SaveChangesAsync();
            }

            // zurück zur Index seite

            return RedirectToPage("./Index");
        }
    }
}
