using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tischtennis_Shop.Models;

namespace Tischtennis_Shop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;



        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public IndexModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

        public IList<Belag> Belag { get; set; }

        public SelectList Belaegemenge { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Markenliste { get; set; }

        public SelectList Artmenge { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Artliste { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Preislistemax { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Preislistemin { get; set; }

      








        public async Task OnGetAsync()
        {
            IQueryable<string> MaterialQuery = from m in _context.Belag
                                               orderby m.Marke
                                               select m.Marke;

            IQueryable<string> ArtQuery = from m in _context.Belag
                                          orderby m.Belag_Art
                                          select m.Belag_Art;

            var Belagmenge = from m in _context.Belag select m;


            if (!string.IsNullOrEmpty(Markenliste))
            {
                Belagmenge = Belagmenge.Where(x => x.Marke == Markenliste);
            }

            if (!string.IsNullOrEmpty(Artliste))
            {
                Belagmenge = Belagmenge.Where(x => x.Belag_Art == Artliste);
            }

            if (!string.IsNullOrEmpty(Preislistemax))
            {
                decimal Preis = Convert.ToDecimal(Preislistemax);

                Belagmenge = Belagmenge.Where(x => x.Preis <= Preis);
            }

            if (!string.IsNullOrEmpty(Preislistemin))
            {
                decimal Preis = Convert.ToDecimal(Preislistemin);

                Belagmenge = Belagmenge.Where(x => x.Preis >= Preis);
            }
            Belag = Belagmenge.ToList();

            Belaegemenge = new SelectList(MaterialQuery.Distinct().ToList());

            Artmenge = new SelectList(ArtQuery.Distinct().ToList());
        }


     
    }
}
