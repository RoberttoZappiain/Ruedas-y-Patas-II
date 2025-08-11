using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuedaYPatas.Data;
using RuedaYPatas.Models;

namespace RuedasYPatas_II.Controllers
{
    [Authorize]
    public class SolicitudesTransporteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SolicitudesTransporteController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: /SolicitudesTransporte/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int MascotaId, int TransporteId)
        {
            if (MascotaId == 0 || TransporteId == 0)
            {
                TempData["ErrorMessage"] = "Debes seleccionar una mascota.";
                return RedirectToAction("DetallesTransporte", "Home", new { id = TransporteId });
            }

            var transporte = await _context.Transportes.FindAsync(TransporteId);
            var userId = _userManager.GetUserId(User);
            
            if (transporte.UsuarioConductorId == userId)
            {
                TempData["ErrorMessage"] = "No puedes solicitar un lugar en tu propio viaje.";
                return RedirectToAction("DetallesTransporte", "Home", new { id = TransporteId });
            }

            bool yaExisteSolicitud = await _context.SolicitudesTransporte
                .AnyAsync(s => s.MascotaId == MascotaId && s.TransporteId == TransporteId);

            if (yaExisteSolicitud)
            {
                TempData["InfoMessage"] = "Ya has enviado una solicitud para esta mascota en este viaje.";
                return RedirectToAction("DetallesTransporte", "Home", new { id = TransporteId });
            }

            var solicitud = new SolicitudTransporte
            {
                EstadoSolicitud = "Pendiente",
                MascotaId = MascotaId,
                TransporteId = TransporteId
            };
            
            _context.SolicitudesTransporte.Add(solicitud);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Tu solicitud ha sido enviada exitosamente.";
            return RedirectToAction("BuscarTransportes", "Home");
        }
    }
}