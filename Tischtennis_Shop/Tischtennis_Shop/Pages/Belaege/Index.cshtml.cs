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
using Tischtennis_Shop.Pages.Mitarbeiterbereich;





namespace Tischtennis_Shop.Pages.Belaege
{
    public class IndexModel : PageModel
    {
        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public IndexModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

        public IList<Belag> Belag { get;set; }

        public IList<int> Mitarbeiter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Gesuchte_ID { get; set; }






        public async Task OnGetAsync()
        {
             
            // Suchen aller Belaege
            var Belagmenge = from m in _context.Belag select m;
            // Suchen aller Mitarbeidter die Edwas geändert haben
            var Mitarbeitermenge = from m in _context.Belag orderby m.ID   select m.Mitarbeiter.ID ;     
            // Hinzufügen der Belagmenge zu IList Belag
            Belag = Belagmenge.ToList();
            // Hinzufügen der Mitarbeidermenge zu IList Mitarbeider
            Mitarbeiter = Mitarbeitermenge.ToList();        
        }
        public async Task OnPostAsync()
        {
            // Suchen des Belages der auf die ID zutrifft
            var Belagmenge = from m in _context.Belag where m.ID == Gesuchte_ID select m;
            // Suchen des Mitarbeiters der auf die Belag ID zutrifft
            var Mitarbeitermenge = from m in _context.Belag where m.ID == Gesuchte_ID  orderby m.ID select m.Mitarbeiter.ID;
            // Hinzufügen der Belagmenge zu IList Belag
            Belag = Belagmenge.ToList();
            // Hinzufügen der Mitarbeidermenge zu IList Mitarbeider
            Mitarbeiter = Mitarbeitermenge.ToList();
        }


    }
}
