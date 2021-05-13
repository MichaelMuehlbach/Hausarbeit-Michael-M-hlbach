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
    public class DetailsModel : PageModel
    {
        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public DetailsModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

        public Belag Belag { get; set; }

        public int MitarbeiterID { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Belag = await _context.Belag.FirstOrDefaultAsync(m => m.ID == id);

           var Mitarbeiermenge = from m in _context.Belag where m.ID == id select m.Mitarbeiter.ID;

            foreach(int i in Mitarbeiermenge) 
            {

                MitarbeiterID = i;
            }

            if (Belag == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
