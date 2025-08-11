using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuedaYPatas.Models;

public class ApplicationUser : IdentityUser
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }

    // --- Propiedades de Navegación (Las relaciones que "salen" de Usuario) ---

    // Un usuario puede tener muchas mascotas
    public ICollection<Mascota> Mascotas { get; set; }

    // Un usuario puede ofrecer muchos hospedajes
    public ICollection<Hospedaje> Hospedajes { get; set; }

    // Un usuario puede conducir muchos transportes
    public ICollection<Transporte> Transportes { get; set; }

    // Relaciones para los comentarios, se necesita InverseProperty para evitar ambigüedad
    [InverseProperty("Autor")]
    public ICollection<Comentario> ComentariosEnviados { get; set; }

    [InverseProperty("Receptor")]
    public ICollection<Comentario> ComentariosRecibidos { get; set; }
}