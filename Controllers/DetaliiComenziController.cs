using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_Cafenea.Data;
using Proiect_Cafenea.Models;

namespace Proiect_Cafenea.Controllers
{
    public class DetaliiComenziController : Controller
    {
        private readonly CafeneaDbContext _context;

        public DetaliiComenziController(CafeneaDbContext context)
        {
            _context = context;
        }

        // GET: DetaliiComenzi
        public async Task<IActionResult> Index()
        {

            var listaDetalii = _context.DetaliiComenzi
                .Include(d => d.Comanda)
                .Include(d => d.Produs)
                .OrderBy(d => d.ComandaId); 
            return View(await listaDetalii.ToListAsync());
        }


        // GET: DetaliiComenzi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detaliiComanda = await _context.DetaliiComenzi
                .Include(d => d.Comanda)
                .Include(d => d.Produs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detaliiComanda == null)
            {
                return NotFound();
            }

            return View(detaliiComanda);
        }

        // GET: DetaliiComenzi/Create
        public IActionResult Create()
        {
            ViewData["ComandaId"] = new SelectList(_context.Comenzi, "Id", "Id");
            ViewData["ProdusId"] = new SelectList(_context.Produse, "Id", "Nume");
            return View();
        }

        // POST: DetaliiComenzi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ComandaId,ProdusId,Cantitate")] DetaliiComanda detaliiComanda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detaliiComanda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComandaId"] = new SelectList(_context.Comenzi, "Id", "Id", detaliiComanda.ComandaId);
            ViewData["ProdusId"] = new SelectList(_context.Produse, "Id", "Nume", detaliiComanda.ProdusId);
            return View(detaliiComanda);
        }

        // GET: DetaliiComenzi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detaliiComanda = await _context.DetaliiComenzi.FindAsync(id);
            if (detaliiComanda == null)
            {
                return NotFound();
            }
            ViewData["ComandaId"] = new SelectList(_context.Comenzi, "Id", "Id", detaliiComanda.ComandaId);
            ViewData["ProdusId"] = new SelectList(_context.Produse, "Id", "Nume", detaliiComanda.ProdusId);
            return View(detaliiComanda);
        }

        // POST: DetaliiComenzi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ComandaId,ProdusId,Cantitate")] DetaliiComanda detaliiComanda)
        {
            if (id != detaliiComanda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detaliiComanda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetaliiComandaExists(detaliiComanda.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComandaId"] = new SelectList(_context.Comenzi, "Id", "Id", detaliiComanda.ComandaId);
            ViewData["ProdusId"] = new SelectList(_context.Produse, "Id", "Nume", detaliiComanda.ProdusId);
            return View(detaliiComanda);
        }

        // GET: DetaliiComenzi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detaliiComanda = await _context.DetaliiComenzi
                .Include(d => d.Comanda)
                .Include(d => d.Produs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detaliiComanda == null)
            {
                return NotFound();
            }

            return View(detaliiComanda);
        }

        // POST: DetaliiComenzi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detaliiComanda = await _context.DetaliiComenzi.FindAsync(id);
            if (detaliiComanda != null)
            {
                _context.DetaliiComenzi.Remove(detaliiComanda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetaliiComandaExists(int id)
        {
            return _context.DetaliiComenzi.Any(e => e.Id == id);
        }
    }
}
