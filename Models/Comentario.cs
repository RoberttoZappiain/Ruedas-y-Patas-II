using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuedaYPatas.Models;

public class Comentario
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Contenido { get; set; }
    
    [Required]
    public DateTime Fecha { get; set; }
    
    [StringLength(50)]
    public string TipoComentario { get; set; } // Ej: "Hospedaje", "Transporte"

    // Clave Foránea para el autor del comentario
    [Required]
    public string AutorId { get; set; }
    [ForeignKey("AutorId")]
    public virtual ApplicationUser Autor { get; set; }

    // Clave Foránea para quien recibe el comentario
    [Required]
    public string ReceptorId { get; set; }
    [ForeignKey("ReceptorId")]
    public virtual ApplicationUser Receptor { get; set; }
}