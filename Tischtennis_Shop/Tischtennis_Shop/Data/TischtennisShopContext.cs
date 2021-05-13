using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tischtennis_Shop.Models;

namespace Tischtennis_Shop.Data
{
    public class TischtennisShopContext : DbContext
    {
        public TischtennisShopContext (DbContextOptions<TischtennisShopContext> options)
            : base(options)
        {
        }

        public DbSet<Tischtennis_Shop.Models.Belag> Belag { get; set; }

        public DbSet<Tischtennis_Shop.Models.Mitarbeiter> Mitarbeiter { get; set; }

        public DbSet<Tischtennis_Shop.Models.Kunde> Kunde { get; set; }

        public DbSet<Tischtennis_Shop.Models.Rechnung> Rechnung { get; set; }

        public DbSet<Tischtennis_Shop.Models.Verkaufte_Ware> Verkaufte_Ware { get; set; }
    }
}
