using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuedaYPatas.Data;
using RuedaYPatas.Models;
using RuedaYPatas.Services;

namespace RuedaYPatas.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class RazasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPetfinderService _petfinderService;

        public RazasController(ApplicationDbContext context, IPetfinderService petfinderService)
        {
            _context = context;
            _petfinderService = petfinderService;
        }

        // GET: Razas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Razas.OrderBy(r => r.Nombre).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SyncFromApi()
        {
            var breedsFromApi = await _petfinderService.GetBreedsAsync("dog");
            if (breedsFromApi == null || !breedsFromApi.Any())
            {
                TempData["ErrorMessage"] = "No se pudo obtener respuesta de la API de Petfinder.";
                return RedirectToAction(nameof(Index));
            }
            
            var existingBreeds = await _context.Razas.Select(r => r.Nombre).ToListAsync();
            var newBreeds = breedsFromApi
                .Where(b => !string.IsNullOrWhiteSpace(b.Name) && !existingBreeds.Contains(b.Name))
                .Select(b => new Raza { Nombre = b.Name });
            
            if (newBreeds.Any())
            {
                await _context.Razas.AddRangeAsync(newBreeds);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"{newBreeds.Count()} nuevas razas han sido sincronizadas.";
            }
            else
            {
                TempData["InfoMessage"] = "No se encontraron nuevas razas para sincronizar. La base de datos ya está actualizada.";
            }
            return RedirectToAction(nameof(Index));
        }
        
        // --- MÉTODOS CRUD COMPLETOS ---
        
        // GET: Razas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var raza = await _context.Razas.FirstOrDefaultAsync(m => m.Id == id);
            if (raza == null) return NotFound();
            return View(raza);
        }

        // GET: Razas/Create
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre")] Raza raza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(raza);
        }

        // GET: Razas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var raza = await _context.Razas.FindAsync(id);
            if (raza == null) return NotFound();
            return View(raza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Raza raza)
        {
            if (id != raza.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Razas.Any(e => e.Id == raza.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(raza);
        }

        // GET: Razas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var raza = await _context.Razas.FirstOrDefaultAsync(m => m.Id == id);
            if (raza == null) return NotFound();
            return View(raza);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var raza = await _context.Razas.FindAsync(id);
            if (raza != null) _context.Razas.Remove(raza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}