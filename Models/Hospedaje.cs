using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RuedaYPatas.Models;

namespace RuedaYPatas.Models; // Asegúrate que el namespace sea el de tu proyecto

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

    // --- PROPIEDAD QUE FALTABA ---
    [StringLength(300)]
    public string Foto { get; set; }
    // ----------------------------

    [Required]
    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual ApplicationUser Usuario { get; set; }

    [Required]
    public int UbicacionId { get; set; }
    [ForeignKey("UbicacionId")]
    public virtual Ubicacion Ubicacion { get; set; }

    public virtual ICollection<ReservaHospedaje> Reservas { get; set; }
}