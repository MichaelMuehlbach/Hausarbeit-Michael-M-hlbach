using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tischtennis_Shop.Data;
using Tischtennis_Shop.Models;

namespace Tischtennis_Shop.Pages.Verkaufte_Waren
{
    public class DeleteModel : PageModel
    {
        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public DeleteModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Verkaufte_Ware Verkaufte_Ware { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Verkaufte_Ware = await _context.Verkaufte_Ware.FirstOrDefaultAsync(m => m.ID == id);

            if (Verkaufte_Ware == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Verkaufte_Ware = await _context.Verkaufte_Ware.FindAsync(id);

            if (Verkaufte_Ware != null)
            {
                _context.Verkaufte_Ware.Remove(Verkaufte_Ware);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
