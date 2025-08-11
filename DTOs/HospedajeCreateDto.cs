using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RuedaYPatas.DTOs
{
    public class HospedajeCreateDto
    {
        [Required]
        [StringLength(150)]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Costo por Noche")]
        public decimal CostoPorNoche { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una ubicación.")]
        [Display(Name = "Ubicación")]
        public int UbicacionId { get; set; }

        public IFormFile FotoFile { get; set; }
    }
}