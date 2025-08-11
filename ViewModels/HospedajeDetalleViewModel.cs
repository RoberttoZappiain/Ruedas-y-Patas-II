using Microsoft.AspNetCore.Mvc.Rendering;
using RuedaYPatas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RuedaYPatas.ViewModels
{
    public class HospedajeDetalleViewModel
    {
        public Hospedaje Hospedaje { get; set; }

        [Required(ErrorMessage = "Debes seleccionar una mascota.")]
        [Display(Name = "¿Para qué mascota?")]
        public int MascotaId { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }

        public IEnumerable<SelectListItem> MascotasDisponibles { get; set; }
    }
}