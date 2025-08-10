using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuedaYPatas.Models;

public class Hospedaje
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Titulo { get; set; }

    [Required]
    public string Descripcion { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal CostoPorNoche { get; set; }

    // Clave Foránea para el anfitrión (ApplicationUser)
    [Required]
    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual ApplicationUser Usuario { get; set; }

    // Clave Foránea para la ubicación
    [Required]
    public int UbicacionId { get; set; }
    [ForeignKey("UbicacionId")]
    public virtual Ubicacion Ubicacion { get; set; }

    // Un hospedaje puede tener muchas reservas
    public virtual ICollection<ReservaHospedaje> Reservas { get; set; }
}