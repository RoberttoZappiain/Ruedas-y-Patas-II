using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuedaYPatas.Models;

public class SolicitudTransporte
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string EstadoSolicitud { get; set; } // Ej: "Aprobada", "Rechazada"

    // Clave Foránea para la mascota
    [Required]
    public int MascotaId { get; set; }
    [ForeignKey("MascotaId")]
    public virtual Mascota Mascota { get; set; }

    // Clave Foránea para el transporte
    [Required]
    public int TransporteId { get; set; }
    [ForeignKey("TransporteId")]
    public virtual Transporte Transporte { get; set; }
}