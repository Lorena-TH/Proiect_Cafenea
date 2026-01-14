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
    public class ProduseController : Controller
    {
        private readonly CafeneaDbContext _context;

        public ProduseController(CafeneaDbContext context)
        {
            _context = context;
        }

        // GET: Produse
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PretSortParm"] = sortOrder == "Pret_Asc" ? "Pret_Desc" : "Pret_Asc";
            ViewData["CurrentFilter"] = searchString;
            var produse = _context.Produse.Include(p => p.Categorie).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                string searchLower = searchString.ToLower();
                produse = produse.Where(s => s.Nume.ToLower().Contains(searchLower)
                                  || s.Categorie.Nume.ToLower().Contains(searchLower));
            }
            produse = sortOrder switch
            {
                "Pret_Asc" => produse.OrderBy(p => p.Pret),           
                "Pret_Desc" => produse.OrderByDescending(p => p.Pret),
                "name_desc" => produse.OrderByDescending(p => p.Nume),
                _ => produse.OrderBy(p => p.Nume)                      
            };
            return View(await produse.ToListAsync());
        }


        // GET: Produse/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs = await _context.Produse
                .Include(p => p.Categorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produs == null)
            {
                return NotFound();
            }

            return View(produs);
        }

        // GET: Produse/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categorii, "Id", "Nume");
            return View();
        }

        // POST: Produse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nume,Pret,CategorieId")] Produs produs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategorieId"] = new SelectList(_context.Categorii, "Id", "Nume", produs.CategorieId);
            return View(produs);
        }

        // GET: Produse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs = await _context.Produse.FindAsync(id);
            if (produs == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categorii, "Id", "Nume", produs.CategorieId);
            return View(produs);
        }

        // POST: Produse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Pret,CategorieId")] Produs produs)
        {
            if (id != produs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdusExists(produs.Id))
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
            ViewData["CategorieId"] = new SelectList(_context.Categorii, "Id", "Nume", produs.CategorieId);
            return View(produs);
        }

        // GET: Produse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs = await _context.Produse
                .Include(p => p.Categorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produs == null)
            {
                return NotFound();
            }

            return View(produs);
        }

        // POST: Produse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produs = await _context.Produse.FindAsync(id);
            if (produs != null)
            {
                _context.Produse.Remove(produs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdusExists(int id)
        {
            return _context.Produse.Any(e => e.Id == id);
        }
    }
}
