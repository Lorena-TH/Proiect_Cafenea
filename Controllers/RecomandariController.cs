using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_Cafenea.Data;
using Proiect_Cafenea.Models;
using Proiect_Cafenea.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Cafenea.Controllers
{
    public class RecomandariController : Controller
    {
        private readonly IRecomandareService _recomandareService;
        private readonly CafeneaDbContext _context;

        public RecomandariController(IRecomandareService recomandareService, CafeneaDbContext context)
        {
            _recomandareService = recomandareService;
            _context = context;
        }

      
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            ViewBag.Clienti = new SelectList(await _context.Clienti.ToListAsync(), "Id", "Nume");

        
            return View(new List<RecomandarePredictionViewModel>());
        }


        [HttpPost]
        public async Task<IActionResult> Index(int? clientId)
        {
            ViewBag.Clienti = new SelectList(await _context.Clienti.ToListAsync(), "Id", "Nume", clientId);

            if (clientId == null)
            {
                return View(new List<RecomandarePredictionViewModel>());
            }


            var toateProdusele = await _context.Produse
                                               .Include(p => p.Categorie)
                                               .ToListAsync();

            var listaRecomandari = new List<RecomandarePredictionViewModel>();

            foreach (var produs in toateProdusele)
            {

                var input = new RecomandareInput
                {
                    ClientID = (float)clientId.Value,
                    ProdusID = (float)produs.Id,
                    Frecventa = 1
                };

                var scor = await _recomandareService.PredictScoreAsync(input);


                int istoricCategorie = _context.DetaliiComenzi
                    .Where(d => d.Comanda.ClientId == clientId.Value && d.Produs.CategorieId == produs.CategorieId)
                    .Count();

                string textMotivatie = "";

                if (istoricCategorie > 0)
                {
                    textMotivatie = $"Clientul preferă categoria '{produs.Categorie?.Nume}'. A mai comandat produse similare de {istoricCategorie} ori.";
                }
                else
                {
                    textMotivatie = $"O sugestie nouă! Deși nu a încercat categoria '{produs.Categorie?.Nume}', AI-ul a detectat o compatibilitate mare.";
                }


                listaRecomandari.Add(new RecomandarePredictionViewModel
                {
                    NumeProdus = produs.Nume,
                    Categorie = produs.Categorie?.Nume ?? "General",
                    Scor = scor,
                    Motivatie = textMotivatie 
                });
            }


            var top3 = listaRecomandari.OrderByDescending(x => x.Scor).Take(3).ToList();

            return View(top3);
        }

      


    }
}