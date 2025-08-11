using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RuedaYPatas.Data;
using RuedaYPatas.Models;
using RuedaYPatas.ViewModels;

namespace RuedaYPatas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var listadoTransportes = await _context.Transportes
                .Include(t => t.Origen)
                .Include(t => t.Destino)
                .Include(t => t.Conductor)
                .OrderBy(t => t.FechaSalida) // Ordenados por fecha de salida
                .ToListAsync();
            
            return View(listadoTransportes);
        }

        public async Task<IActionResult> DetallesHospedaje(int? id)
        {
            if (id == null) return NotFound();

            var hospedaje = await _context.Hospedajes
                .Include(h => h.Ubicacion)
                .Include(h => h.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
                    
            if (hospedaje == null) return NotFound();

            var viewModel = new HospedajeDetalleViewModel
            {
                Hospedaje = hospedaje
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                var mascotasUsuario = await _context.Mascotas
                    .Where(m => m.UsuarioId == userId)
                    .OrderBy(m => m.Nombre)
                    .ToListAsync();
                
                viewModel.MascotasDisponibles = new SelectList(mascotasUsuario, "Id", "Nombre");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> BuscarTransportes()
        {
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> DetallesTransporte(int? id)
        {
            if (id == null) return NotFound();
            var transporte = await _context.Transportes.Include(t => t.Origen).Include(t => t.Destino).Include(t => t.Conductor).FirstOrDefaultAsync(m => m.Id == id);
            if (transporte == null) return NotFound();
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                ViewBag.MascotasUsuario = new SelectList(
                    await _context.Mascotas.Where(m => m.UsuarioId == userId).ToListAsync(), "Id", "Nombre");
            }
            return View(transporte);
        }
    }
}