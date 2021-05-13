using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tischtennis_Shop.Data;
using Tischtennis_Shop.Models;

namespace Tischtennis_Shop.Pages.Kunden
{
    public class IndexModel : PageModel
    {
        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public IndexModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

        public IList<Kunde> Kunde { get;set; }

        public async Task OnGetAsync()
        {
            Kunde = await _context.Kunde.ToListAsync();
        }
    }
}
