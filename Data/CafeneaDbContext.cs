using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect_Cafenea.Models;
namespace Proiect_Cafenea.Data
{
    public class CafeneaDbContext : DbContext
    {
        public CafeneaDbContext(DbContextOptions<CafeneaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categorie> Categorii { get; set; } = default!;
        public DbSet<Proiect_Cafenea.Models.Produs> Produse { get; set; } = default!;
        public DbSet<Proiect_Cafenea.Models.Client> Clienti { get; set; } = default!;
        public DbSet<Proiect_Cafenea.Models.Comanda> Comenzi { get; set; } = default!;
        public DbSet<Proiect_Cafenea.Models.DetaliiComanda> DetaliiComenzi { get; set; } = default!;
    }
}
