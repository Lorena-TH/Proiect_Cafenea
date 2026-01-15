using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_Cafenea.Models;
using Proiect_Cafenea.Data;

namespace Proiect_Cafenea.Controllers
{
    public class HomeController : Controller
    {
        private readonly CafeneaDbContext _context;

        public HomeController(CafeneaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // calculeaza totalurile pt cardurile de statistici
            var totalClienti = await _context.Clienti.CountAsync();
            var totalComenzi = await _context.Comenzi.CountAsync();
            var totalProduse = await _context.Produse.CountAsync();

            // grupeaza produsele dupa categorie
            var categoriiStats = await _context.DetaliiComenzi
        .Include(dc => dc.Produs)
        .ThenInclude(p => p.Categorie)
        .GroupBy(dc => dc.Produs.Categorie.Nume)
        .Select(g => new CategorieStat
        {
            NumeCategorie = g.Key ?? "Altele",

            NumarProduse = (int)g.Sum(dc => dc.Cantitate * dc.Produs.Pret)
        })
        .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                TotalClienti = totalClienti,
                TotalComenzi = totalComenzi,
                TotalProduse = totalProduse,
                CategoriiStats = categoriiStats
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}