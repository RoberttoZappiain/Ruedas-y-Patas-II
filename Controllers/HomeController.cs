using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RuedasYPatas.Models;
using RuedaYPatas.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RuedaYPatas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPetfinderService _petfinderService; // <-- DEBES DECLARAR EL CAMPO PRIVADO

        // El constructor debe recibir el servicio y asignarlo al campo privado
        public HomeController(ILogger<HomeController> logger, IPetfinderService petfinderService)
        {
            _logger = logger;
            _petfinderService = petfinderService; // <-- DEBES ASIGNARLO AQUÍ
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // --- TU MÉTODO DE PRUEBA ---
        public async Task<IActionResult> TestApi()
        {
            try
            {
                var dogBreeds = await _petfinderService.GetBreedsAsync("dog");
                ViewBag.Breeds = dogBreeds;
                ViewBag.Success = true;
            }
            catch (System.Exception ex)
            {
                ViewBag.Success = false;
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}