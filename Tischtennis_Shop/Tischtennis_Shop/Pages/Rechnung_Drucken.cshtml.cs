using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tischtennis_Shop.Models;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Colors;
using iText.Layout.Properties;

namespace Tischtennis_Shop.Pages
{

  
    public class Rechnung_DruckenModel : PageModel
    {

        private readonly Tischtennis_Shop.Data.TischtennisShopContext _context;

        public Rechnung_DruckenModel(Tischtennis_Shop.Data.TischtennisShopContext context)
        {
            _context = context;
        }
      

        public IList<Verkaufte_Ware> Ware { get; set; }

        public Rechnung Rechnung { get; set; }

        public Kunde Kunde { get; set; }
        public void OnGet()
        {
            // Alle rechnungen aus der Datenbank rausgeschrieben
            var Rechnungen = from R in _context.Rechnung select R;
            // Rechnung besetzen mit der letzen Rechnung durch das man alle durchgeht und nur die letzte gibt dann ihren werd an Rechnung weiter
            foreach (Rechnung r in Rechnungen)
            {
                Rechnung = r;
            }
            // Alle kunden rausschreiben aus der Datenbank
            var Kunden = from K in _context.Kunde select K;
            // Waren aus der Datenabank schreiben die die rechnungs Id haben
            var Waren = from W in _context.Verkaufte_Ware where W.Rechnung == Rechnung select W;
            // Kunden Suchen der die rechnung bestellt hat
            foreach (Kunde k in Kunden)
            {
                if (Rechnung.Kunde == k)
                {
                    Kunde = k;
                }
            }
            // Hinzufügen der Waren zur IList Ware
            Ware = Waren.ToList();              
            // Die Varaible dest Erstellen und mit dme Pfad des PDF Dokumendes versehen
            string dest = @"C:\Users\user\Desktop\Rechnung.pdf";
            // Ein PDF writer erstellen 
            PdfWriter writer = new PdfWriter(dest);
            // Ein PDF Dokument erstellen
            PdfDocument pdf = new PdfDocument(writer);
            // Document erstellen
            Document document = new Document(pdf);
            // Kunden Daten dem PDF Document Hinzufügeb
            document.Add(new Paragraph(Kunde.Vorname + " " + Kunde.Nachname  ));
            document.Add(new Paragraph(Kunde.Straße + " " + Kunde.Hausnummer));
            document.Add(new Paragraph(Kunde.Postleitzahl + " " + Kunde.Ort));
            document.Add(new Paragraph());
            document.Add(new Paragraph("Kunden Nummer" + ": " + Kunde.ID));
            document.Add(new Paragraph());
            document.Add(new Paragraph("Rechnungs Nummer" + ": " + Rechnung.ID));
            document.Add(new Paragraph());
            document.Add(new Paragraph());
            document.Add(new Paragraph());
            // Tabelle mit den Gekauften Belägen erstellen
            Table table = new Table(5, false);
            Cell cell11 = new Cell(1, 1) 
            .SetTextAlignment(TextAlignment.CENTER)  
            .Add(new Paragraph("Name"));
            Cell cell12 = new Cell(1, 1)   
            .SetTextAlignment(TextAlignment.CENTER)
            .Add(new Paragraph("Farbe"));
            Cell cell21 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.CENTER)
            .Add(new Paragraph("Menge"));
            Cell cell22 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.CENTER)
            .Add(new Paragraph("Preis je Stück"));
            Cell cell31 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.CENTER)
            .Add(new Paragraph("Gesamt Pries"));
            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell21);
            table.AddCell(cell22);
            table.AddCell(cell31);
            foreach (Verkaufte_Ware w in Waren) 
            {
            Cell cell1 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.CENTER)
            .Add(new Paragraph(w.Name));
            Cell cell2 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.CENTER)
            .Add(new Paragraph(w.Farbe));
            Cell cell3 = new Cell(1, 1)
            .SetTextAlignment(TextAlignment.CENTER)
            .Add(new Paragraph( Convert.ToString( w.Menge)));
            Cell cell4 = new Cell(1, 1)
           .SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph(Convert.ToString( w.Preis_je_Stueck)));
           Cell cell5 = new Cell(1, 1)
           .SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph(Convert.ToString(w.Gesamtpreis)));
           table.AddCell(cell1);
           table.AddCell(cell2);
           table.AddCell(cell3);
           table.AddCell(cell4);
           table.AddCell(cell5);
            }
            // table dem Document Hinzufügen
            document.Add(table);
            document.Add(new Paragraph());
            document.Add(new Paragraph());
            // Gesamt Betrag Hinzufügen
            document.Add(new Paragraph("Gesamtbetrag in Euro" + ": " + Rechnung.Gesamtbetrag + "€"));
            // document schließen
            document.Close();
        }
    }
}
