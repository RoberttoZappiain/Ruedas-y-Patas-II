using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuedaYPatas.Data;
using RuedaYPatas.Models;
using RuedaYPatas.ViewModels;

namespace RuedaYPatas.Controllers
{
    [Authorize]
    public class ReservasHospedajeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservasHospedajeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HospedajeDetalleViewModel viewModel)
        {
            var hospedajeId = viewModel.Hospedaje.Id;
            ModelState.Remove("Hospedaje");

            if (ModelState.IsValid)
            {
                var hospedaje = await _context.Hospedajes.FindAsync(hospedajeId);
                var userId = _userManager.GetUserId(User);

                if (hospedaje.UsuarioId == userId)
                {
                     TempData["ErrorMessage"] = "No puedes reservar tu propio servicio.";
                    return RedirectToAction("DetallesHospedaje", "Home", new { id = hospedajeId });
                }

                var reserva = new ReservaHospedaje
                {
                    FechaInicio = viewModel.FechaInicio,
                    FechaFin = viewModel.FechaFin,
                    EstadoReserva = "Confirmada",
                    MascotaId = viewModel.MascotaId,
                    HospedajeId = hospedajeId,
                };

                _context.ReservasHospedaje.Add(reserva);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "¡Tu reserva ha sido confirmada!";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Hubo un error con los datos de tu reserva.";
            return RedirectToAction("DetallesHospedaje", "Home", new { id = hospedajeId });
        }
    }
}