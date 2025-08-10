using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RuedaYPatas.Data;
using RuedaYPatas.Models;
using RuedaYPatas.Services;
using RuedaYPatas.ViewModels;

namespace RuedaYPatas.Controllers
{
    [Authorize]
    public class MascotasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IImageService _imageService;
        private readonly IPetfinderService _petfinderService;

        public MascotasController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IImageService imageService,
            IPetfinderService petfinderService)
        {
            _context = context;
            _userManager = userManager;
            _imageService = imageService;
            _petfinderService = petfinderService;
        }

        // GET: Mascotas
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var mascotas = await _context.Mascotas
                .Where(m => m.UsuarioId == userId)
                .Include(m => m.RazaEntidad)
                .ToListAsync();
            return View(mascotas);
        }

        // GET: Mascotas/Create
        public async Task<IActionResult> Create()
        {
            var breedsFromApi = await _petfinderService.GetBreedsAsync("dog");
            var viewModel = new MascotaCreateViewModel
            {
                RazasDisponibles = breedsFromApi.Select(b => new SelectListItem
                {
                    Text = b.Name,
                    Value = b.Name
                }).OrderBy(b => b.Text)
            };
            return View(viewModel);
        }

        // POST: Mascotas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Nombre, string Especie, int Edad, string Raza, IFormFile FotoFile)
        {
            // Si llegamos aquí, ¡el envío del formulario funcionó!

            // 1. Validar manualmente los datos necesarios
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Raza))
            {
                // Si la validación falla, recargamos la vista con un error.
                TempData["ErrorMessage"] = "El nombre y la raza son obligatorios.";
                // (En un caso real, recargaríamos el dropdown aquí, pero esto es para confirmar que funciona)
                return RedirectToAction("Create");
            }

            // 2. Buscar o crear la raza en la base de datos local
            var razaSeleccionada = await _context.Razas
                .FirstOrDefaultAsync(r => r.Nombre == Raza);

            if (razaSeleccionada == null)
            {
                razaSeleccionada = new Models.Raza { Nombre = Raza };
                _context.Razas.Add(razaSeleccionada);
                await _context.SaveChangesAsync();
            }

            // 3. Crear el objeto Mascota real para guardarlo
            var nuevaMascota = new Mascota
            {
                Nombre = Nombre,
                Especie = Especie,
                Edad = Edad,
                Raza = Raza, // Guardamos el nombre
                RazaId = razaSeleccionada.Id, // Guardamos el Id de la relación
                UsuarioId = _userManager.GetUserId(User)
            };

            // 4. Guardar la imagen si existe
            if (FotoFile != null)
            {
                nuevaMascota.Foto = await _imageService.GuardarImagenAsync(FotoFile);
            }

            // 5. Guardar la mascota y redirigir
            _context.Mascotas.Add(nuevaMascota);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Mascota registrada exitosamente.";
            return RedirectToAction(nameof(Index));
        }
                // GET: Mascotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null) return NotFound();

            // Asegurarse de que el usuario solo pueda editar sus propias mascotas
            if (mascota.UsuarioId != _userManager.GetUserId(User))
            {
                return Forbid(); // Prohibido
            }

            ViewBag.Raza = new SelectList(await _context.Razas.OrderBy(r => r.Nombre).ToListAsync(), "Nombre", "Nombre", mascota.Raza);
            return View(mascota);
        }

        // POST: Mascotas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mascota mascota, IFormFile fotoFile)
        {
            if (id != mascota.Id) return NotFound();

            // Asegurarse de que el usuario solo pueda editar sus propias mascotas
            var mascotaOriginal = await _context.Mascotas.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (mascotaOriginal.UsuarioId != _userManager.GetUserId(User))
            {
                return Forbid();
            }
            
            // Asignamos el ID de usuario para que no se pierda
            mascota.UsuarioId = mascotaOriginal.UsuarioId;

            if (ModelState.IsValid)
            {
                try
                {
                    if (fotoFile != null)
                    {
                        mascota.Foto = await _imageService.GuardarImagenAsync(fotoFile);
                    }
                    else
                    {
                        // Si no se sube una nueva foto, mantenemos la antigua
                        mascota.Foto = mascotaOriginal.Foto;
                    }

                    _context.Update(mascota);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Mascota actualizada exitosamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Mascotas.Any(e => e.Id == mascota.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Raza = new SelectList(await _context.Razas.OrderBy(r => r.Nombre).ToListAsync(), "Nombre", "Nombre", mascota.Raza);
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var mascota = await _context.Mascotas
                .Include(m => m.RazaEntidad)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (mascota == null) return NotFound();
            
            if (mascota.UsuarioId != _userManager.GetUserId(User)) return Forbid();

            return View(mascota);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null) return NotFound();

            if (mascota.UsuarioId != _userManager.GetUserId(User)) return Forbid();

            _context.Mascotas.Remove(mascota);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Mascota eliminada exitosamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}