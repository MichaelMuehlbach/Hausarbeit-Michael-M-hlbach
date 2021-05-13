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


        [BindProperty]
        public Belag Belag { get; set; }

        public Mitarbeiter Mitarbeiter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Belag = await _context.Belag.FirstOrDefaultAsync(m => m.ID == id);


            var Mitarbeiermenge = from m in _context.Belag where m.ID == id select m.Mitarbeiter.ID;

            foreach (int i in Mitarbeiermenge)
            {

                Mitarbeiderid = Convert.ToString( i);
            }



            if (Belag == null )
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            int Mid = 0;

          

            if (Mitarbeiderid == null) 
            {
                return RedirectToPage("/FehlerMitarbeiternummer");
            }
            else 
            {
                Mid = Convert.ToInt32(Mitarbeiderid);
                

            }

          

            Mitarbeiter = await _context.Mitarbeiter.FirstOrDefaultAsync(x => x.ID == Mid);
           

            if(Mitarbeiter ==  null) 
            {
              return  RedirectToPage("/FehlerMitarbeiternummer");
            }
              
            Belag.Mitarbeiter = Mitarbeiter;

              
            
            
            

            if (!ModelState.IsValid)
            {
                return Page();
            }


            
            _context.Attach(Belag).State = EntityState.Modified;

            try
            {
               
                
                await _context.SaveChangesAsync();

             
            }
            catch (DbUpdateConcurrencyException)
            {
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
            return _context.Belag.Any(e => e.ID == id);
        }

       
    }
}
