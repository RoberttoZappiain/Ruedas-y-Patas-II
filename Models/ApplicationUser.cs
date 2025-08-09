using Microsoft.AspNetCore.Identity;

namespace RuedaYPatas.Models;

// Heredamos de IdentityUser para añadir campos personalizados
public class ApplicationUser : IdentityUser
{
    public string NombreCompleto { get; set; }
    public string Direccion { get; set; }
}