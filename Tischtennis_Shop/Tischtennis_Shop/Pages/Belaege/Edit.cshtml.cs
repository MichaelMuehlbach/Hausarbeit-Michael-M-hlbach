using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tischtennis_Shop.Data;
using Tischtennis_Shop.Models;

namespace Tischtennis_Shop.Pages.Belaege
{
    public class EditModel : PageModel
    {
        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public EditModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string Mitarbeiderid { get; set; }

        [BindProperty(SupportsGet = true)]
        public string eingabepreis { get; set; }



        [BindProperty]
        public Belag Belag { get; set; }

        public Mitarbeiter Mitarbeiter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            // Abfangen des Falles ID ist gleich null

            if (id == null)
            {
                return NotFound();
            }

            // Suchen des Belages

            Belag = await _context.Belag.FirstOrDefaultAsync(m => m.ID == id);

            // Suchen des Besanden Mitarbeiters

            var Mitarbeiermenge = from m in _context.Belag where m.ID == id select m.Mitarbeiter.ID;

            foreach (int i in Mitarbeiermenge)
            {
                // Convertirung der Mitarbeiter ID

                Mitarbeiderid = Convert.ToString( i);
            }

            // Abfangen fall Belag ist nicht vorhanden

            if (Belag == null )
            {
                return NotFound();
            }
            // Festlegen der Variable eingabepreis 
            eingabepreis = Convert.ToString(Belag.Preis);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            int Mid = 0;

            // Abfangen des Falles Mitarbeider ID ist gleich null

            if (Mitarbeiderid == null) 
            {
                return RedirectToPage("/FehlerMitarbeiternummer");
            }
            else 
            {
                Mid = Convert.ToInt32(Mitarbeiderid);
                

            }

            // Suchen des Mitarbeiders

            Mitarbeiter = await _context.Mitarbeiter.FirstOrDefaultAsync(x => x.ID == Mid);
           
            // Abfangen fall Mitarbeider ist nicht vorhanden
            if(Mitarbeiter ==  null) 
            {
              return  RedirectToPage("/FehlerMitarbeiternummer");
            }

            // Belag.Mitarbeider wird gesetzt
              
            Belag.Mitarbeiter = Mitarbeiter;


            // Festlegen des Preises des Belages durch umwandeln der String eingabe

            Belag.Preis = Convert.ToDecimal(eingabepreis);  
            
            
            // konntrolle ob alles mit der Valedierung stimmt

            if (!ModelState.IsValid)
            {
                return Page();
            }


            // Aktualiesieren des Belages
            _context.Attach(Belag).State = EntityState.Modified;

            try
            {
               // Änderungen speichern
                
                await _context.SaveChangesAsync();

             
            }
            // Abfangen des Fehlers beim speichern in der Datenbank

            catch (DbUpdateConcurrencyException)
            {

                // Abfangen das der Belag nicht exsiestiert
                if (!BelagExists(Belag.ID) )
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BelagExists(int id)
        {

            // Schauen ob belag dar ist

            return _context.Belag.Any(e => e.ID == id);
        }

       
    }
}
