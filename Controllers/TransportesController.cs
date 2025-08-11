using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RuedaYPatas.Data;
using RuedaYPatas.Models;
using RuedaYPatas.DTOs;

namespace RuedasYPatas.Controllers
{
    [Authorize]
    public class TransportesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TransportesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var transportes = await _context.Transportes
                .Where(t => t.UsuarioConductorId == userId)
                .Include(t => t.Origen)
                .Include(t => t.Destino)
                .ToListAsync();
            return View(transportes);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateUbicacionesViewData();
            return View(new TransporteCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransporteCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                var transporte = new Transporte
                {
                    Descripcion = dto.Descripcion,
                    FechaSalida = dto.FechaSalida,
                    FechaLlegada = dto.FechaLlegada,
                    Capacidad = dto.Capacidad,
                    UbicacionOrigenId = dto.UbicacionOrigenId,
                    UbicacionDestinoId = dto.UbicacionDestinoId,
                    UsuarioConductorId = _userManager.GetUserId(User)
                };
                _context.Add(transporte);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Transporte creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            await PopulateUbicacionesViewData(dto.UbicacionOrigenId, dto.UbicacionDestinoId);
            return View(dto);
        }
        
        private async Task PopulateUbicacionesViewData(object selectedOrigen = null, object selectedDestino = null)
        {
            var ubicaciones = await _context.Ubicaciones.OrderBy(u => u.Ciudad).ToListAsync();
            ViewData["UbicacionOrigenId"] = new SelectList(ubicaciones, "Id", "Ciudad", selectedOrigen);
            ViewData["UbicacionDestinoId"] = new SelectList(ubicaciones, "Id", "Ciudad", selectedDestino);
        }
    }
}