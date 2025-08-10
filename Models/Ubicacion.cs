using System.ComponentModel.DataAnnotations;

namespace RuedaYPatas.Models;

public class Ubicacion
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Ciudad { get; set; }

    [Required]
    [StringLength(100)]
    public string Estado { get; set; }
}