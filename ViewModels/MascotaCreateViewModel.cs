using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RuedaYPatas.ViewModels
{
    public class MascotaCreateViewModel
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Especie { get; set; } = "Perro";

        [Required(ErrorMessage = "Debe seleccionar una raza.")]
        [Display(Name = "Raza")]
        public string Raza { get; set; } // Aquí guardaremos el nombre de la raza seleccionada

        public int Edad { get; set; }

        [Display(Name = "Foto de la Mascota")]
        public IFormFile FotoFile { get; set; }

        // Esta lista contendrá las opciones para el dropdown, cargadas desde la API
        public IEnumerable<SelectListItem> RazasDisponibles { get; set; }
    }
}