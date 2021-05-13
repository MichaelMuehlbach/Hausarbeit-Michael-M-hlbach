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

            var Belagmenge = from m in _context.Belag select m;

            var Mitarbeitermenge = from m in _context.Belag orderby m.ID   select m.Mitarbeiter.ID ;       

            Belag = Belagmenge.ToList();


            

            Mitarbeiter = Mitarbeitermenge.ToList();

           
        }


        public async Task OnPostAsync()
        {
            var Belagmenge = from m in _context.Belag where m.ID == Gesuchte_ID select m;

            var Mitarbeitermenge = from m in _context.Belag where m.ID == Gesuchte_ID  orderby m.ID select m.Mitarbeiter.ID;

            Belag = Belagmenge.ToList();




            Mitarbeiter = Mitarbeitermenge.ToList();



        }


    }
}
