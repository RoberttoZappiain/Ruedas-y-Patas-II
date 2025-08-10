using System.ComponentModel.DataAnnotations;

namespace RuedaYPatas.Models;

public class Raza
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Nombre de la Raza")]
    public string Nombre { get; set; }
}