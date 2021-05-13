using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tischtennis_Shop.Data;
using Tischtennis_Shop.Models;
using System.Web;
using Microsoft.Data.SqlClient.DataClassification;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Tischtennis_Shop.Pages.Mitarbeiterbereich
{
    public class IndexModel : PageModel
    {
        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public IndexModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }


        [RegularExpression(@"^[A-Z]+[a-z\s]*$"), Required, StringLength(20)] 
        [BindProperty(SupportsGet = true)]
        public string Vorname { get; set; }


        [RegularExpression(@"^[A-Z]+[a-z\s]*$")]
        [Required]
        [StringLength(20)]
        [BindProperty(SupportsGet = true)]
        public string Nachname { get; set; }

        public Mitarbeiter Mitarbeiterakttuel {protected get; set; }

        [BindProperty(SupportsGet = true)]
        public string PasswortString { get; set; }

        public IList<Mitarbeiter> Mitarbeiter { get;set; }

        public async Task OnGetAsync()
        {
          
                

            
         

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var Mitarbeitermenge = from m in _context.Mitarbeiter select m;

            MD5 md5 = new MD5CryptoServiceProvider();


            Mitarbeiter MitarbeiterAktuell = Mitarbeitermenge.FirstOrDefault(x => x.Nachname == Nachname && x.Vorname == Vorname);




            if (MitarbeiterAktuell != null)
            {

                byte[] hash = md5.ComputeHash(Encoding.Unicode.GetBytes(PasswortString));
                string b64hash = Convert.ToBase64String(hash);

                if (MitarbeiterAktuell.Passwort == b64hash)
                {
                    
                    return RedirectToPage("/Belaege/Index");
                }


                return RedirectToPage("/Falsche_Eingabe_Mitarbeiterbereich");

            }

            else
            {
              

                return RedirectToPage("/Falsche_Eingabe_Mitarbeiterbereich");
            }


        }
    }
}
