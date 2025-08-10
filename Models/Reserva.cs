using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuedaYPatas.Models;

public class ReservaHospedaje
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime FechaInicio { get; set; }
    [Required]
    public DateTime FechaFin { get; set; }

    [Required]
    [StringLength(50)]
    public string EstadoReserva { get; set; } // Ej: "Confirmada", "Pendiente"

    // Clave Foránea para la mascota
    [Required]
    public int MascotaId { get; set; }
    [ForeignKey("MascotaId")]
    public virtual Mascota Mascota { get; set; }

    // Clave Foránea para el hospedaje
    [Required]
    public int HospedajeId { get; set; }
    [ForeignKey("HospedajeId")]
    public virtual Hospedaje Hospedaje { get; set; }
}