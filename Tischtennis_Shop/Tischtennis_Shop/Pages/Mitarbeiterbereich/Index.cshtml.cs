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

using System.Diagnostics;

namespace Tischtennis_Shop.Pages.Mitarbeiterbereich
{
    public class IndexModel : PageModel
    {
        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public IndexModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }

        private static int numberOfIterations = 10000;

        // Validierung Hinzugefügt am Anfang muss ein groß Bustabe sein und danach klein Bustaben


        [RegularExpression(@"^[A-Z]+[a-z\s]*$")]     
        [Required(ErrorMessage = "Bitte geben sie ihren Vornamen ein")]
        [StringLength(20)]
        [BindProperty(SupportsGet = true)]
        public string Vorname { get; set; }




        // Validierung Hinzugefügt am Anfang muss ein groß Bustabe sein und danach klein Bustaben

        [RegularExpression(@"^[A-Z]+[a-z\s]*$")]
        [Required (ErrorMessage = "Bitte geben sie ihren Nachnamen ein.")]
        [StringLength(20)]
        [BindProperty(SupportsGet = true)]
        public string Nachname { get; set; }

        public Mitarbeiter Mitarbeiterakttuel {protected get; set; }


        [Required(ErrorMessage = "Bitte geben sie ihren Passwort ein")]
        [BindProperty(SupportsGet = true)]
        public string PasswortString { get; set; }

        public IList<Mitarbeiter> Mitarbeiter { get;set; }

        public async Task OnGetAsync()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Kontrollieren der Validierung
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Alle Mitarbeiter werden rausgesucht
            var Mitarbeitermenge = from m in _context.Mitarbeiter select m;

            //    Heraussuchen des Passenden Mitarbeiters laut eingab
            Mitarbeiter MitarbeiterAktuell = Mitarbeitermenge.FirstOrDefault(x => x.Nachname == Nachname && x.Vorname == Vorname);
            // Abfangen des Falles Mitarbeiter nicht vorhanden
            if (MitarbeiterAktuell != null)
            {

                // Umwandeln des Salt in Bytes
                byte[] saltBytes = Convert.FromBase64String(MitarbeiterAktuell.Salt);

                //  Bestimmung des Passwort-Hash-Wertes für das eingegebene Passwort
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(PasswortString, saltBytes);
                // Werte müssen identisch zu den Werten beim Generieren des Passwortes sein
                rfc2898DeriveBytes.IterationCount = numberOfIterations;
                byte[] enteredHash = rfc2898DeriveBytes.GetBytes(20);
                // Umwandeln von Byte-Array in String
                string str = Convert.ToBase64String(enteredHash);

                // Konntrolle ob Passwort richtig und damit Login erfolgreich
                if (MitarbeiterAktuell.Passwort == str)
                {
                    return RedirectToPage("/Belaege/Index");
                }
                // Ansonsten Fehler anzeige
                return RedirectToPage("/Falsche_Eingabe_Mitarbeiterbereich");
            }
            // Ansonsten Fehler anzeige
            else
            {
                return RedirectToPage("/Falsche_Eingabe_Mitarbeiterbereich");
            }
        }
    }
}
