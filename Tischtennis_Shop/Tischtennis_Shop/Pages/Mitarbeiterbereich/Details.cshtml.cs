using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tischtennis_Shop.Data;
using Tischtennis_Shop.Models;

namespace Tischtennis_Shop.Pages.Mitarbeiterbereich
{
    public class DetailsModel : PageModel
    {
        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public DetailsModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

        public Mitarbeiter Mitarbeiter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mitarbeiter = await _context.Mitarbeiter.FirstOrDefaultAsync(m => m.ID == id);

            if (Mitarbeiter == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
