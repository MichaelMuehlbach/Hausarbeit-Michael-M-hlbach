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

        public Mitarbeiter Mitarbeiter { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Belag Belag { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            int Mid = 0;



            if (Mitarbeiderid == null)
            {
                return RedirectToPage("/FehlerMitarbeiternummer");
            }
            else
            {
                Mid = Convert.ToInt32(Mitarbeiderid);


            }



            Mitarbeiter = _context.Mitarbeiter.FirstOrDefault(x => x.ID == Mid);


            if (Mitarbeiter == null)
            {
                return RedirectToPage("/FehlerMitarbeiternummer");
            }

            Belag.Mitarbeiter = Mitarbeiter;






            _context.Belag.Add(Belag);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
