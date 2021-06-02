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

        public SelectList Markemenge { get; set; }



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
            // Hier wird jede ein einzelne Marke Herausgesucht.
            IQueryable<string> MarkeQuery = from m in _context.Belag
                                               orderby m.Marke
                                               select m.Marke;
            // Hier wird jede einzelne Belag-Art Herausgesucht.
            IQueryable<string> ArtQuery = from m in _context.Belag
                                          orderby m.Belag_Art
                                          select m.Belag_Art;
            // Hier werden alle Beläge aus der Datenbank herausgelesen.
            var Belagmenge = from m in _context.Belag select m;
            // Hier wird nach der Marke gefiltert.
            if (!string.IsNullOrEmpty(Markenliste))
            {
                Belagmenge = Belagmenge.Where(x => x.Marke == Markenliste);
            }
            // Hier wird nach der Belag Art gefiltert.
            if (!string.IsNullOrEmpty(Artliste))
            {
                Belagmenge = Belagmenge.Where(x => x.Belag_Art == Artliste);
            }
            // Hier wird nach dem maximal Preis gefiltert.
            if (!string.IsNullOrEmpty(Preislistemax))
            {
                decimal Preis = Convert.ToDecimal(Preislistemax);
                Belagmenge = Belagmenge.Where(x => x.Preis <= Preis);
            }
            // Hier wird nach dem minimal Preis gefiltert.
            if (!string.IsNullOrEmpty(Preislistemin))
            {
                decimal Preis = Convert.ToDecimal(Preislistemin);
                Belagmenge = Belagmenge.Where(x => x.Preis >= Preis);
            }
            // Hier wird alles der IList Belag Hinzugefügt
            Belag = Belagmenge.ToList();
            // Hier werden alle Marken der SelectList Markenmenge Hinzugefügt.
            Markemenge = new SelectList(MarkeQuery.Distinct().ToList());
            // Hier werden alle Belag Arten der SelectList Artenge hinzugefügt.
            Artmenge = new SelectList(ArtQuery.Distinct().ToList());
        }


     
    }
}
