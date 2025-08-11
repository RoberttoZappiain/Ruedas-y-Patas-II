using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RuedaYPatas.Data;
using RuedaYPatas.Models;
using RuedaYPatas.Services;
using RuedaYPatas.DTOs;

namespace RuedaYPatas.Controllers
{
    [Authorize]
    public class HospedajesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IImageService _imageService;

        public HospedajesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IImageService imageService)
        {
            _context = context; _userManager = userManager; _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var hospedajes = await _context.Hospedajes.Where(h => h.UsuarioId == userId).Include(h => h.Ubicacion).ToListAsync();
            return View(hospedajes);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["UbicacionId"] = new SelectList(await _context.Ubicaciones.OrderBy(u => u.Ciudad).ToListAsync(), "Id", "Ciudad");
            return View(new HospedajeCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HospedajeCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                var nuevoHospedaje = new Hospedaje
                {
                    Titulo = dto.Titulo,
                    Descripcion = dto.Descripcion,
                    CostoPorNoche = dto.CostoPorNoche,
                    UbicacionId = dto.UbicacionId,
                    UsuarioId = _userManager.GetUserId(User)
                };
                if (dto.FotoFile != null)
                {
                    nuevoHospedaje.Foto = await _imageService.GuardarImagenAsync(dto.FotoFile, "hospedajes");
                }
                _context.Add(nuevoHospedaje);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Hospedaje creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["UbicacionId"] = new SelectList(await _context.Ubicaciones.OrderBy(u => u.Ciudad).ToListAsync(), "Id", "Ciudad", dto.UbicacionId);
            return View(dto);
        }
    }
}