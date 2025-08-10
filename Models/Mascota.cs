using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuedaYPatas.Models;

public class Mascota
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]
    [StringLength(50)]
    public string Especie { get; set; }

    [Required]
    [StringLength(100)]
    public string Raza { get; set; }

    public int Edad { get; set; }

    [StringLength(300)]
    public string Foto { get; set; }

    // --- Clave Foránea para el dueño ---
    [Required]
    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual ApplicationUser Usuario { get; set; }

    // --- PROPIEDADES FALTANTES ---
    // Clave Foránea para la raza
    [Required]
    [Display(Name = "Raza")]
    public int RazaId { get; set; }
    [ForeignKey("RazaId")]
    public virtual Raza RazaEntidad { get; set; } 
    // Nota: Le cambié el nombre a RazaEntidad para evitar conflicto 
    // con la propiedad 'string Raza' de arriba.
}