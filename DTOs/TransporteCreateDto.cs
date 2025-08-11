using System;
using System.ComponentModel.DataAnnotations;

namespace RuedaYPatas.DTOs
{
    public class TransporteCreateDto
    {
        [Required]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Fecha y Hora de Salida")]
        public DateTime FechaSalida { get; set; }

        [Required]
        [Display(Name = "Fecha y Hora de Llegada")]
        public DateTime FechaLlegada { get; set; }

        [Required]
        [Display(Name = "Lugares Disponibles")]
        public int Capacidad { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un origen.")]
        [Display(Name = "Ubicación de Origen")]
        public int UbicacionOrigenId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un destino.")]
        [Display(Name = "Ubicación de Destino")]
        public int UbicacionDestinoId { get; set; }
    }
}