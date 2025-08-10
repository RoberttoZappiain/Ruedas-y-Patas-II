using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuedaYPatas.Models;

public class Transporte
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Descripcion { get; set; }

    [Required]
    public DateTime FechaSalida { get; set; }
    
    [Required]
    public DateTime FechaLlegada { get; set; }

    [Required]
    public int Capacidad { get; set; } // Número de mascotas que puede llevar

    // Clave Foránea para el conductor
    [Required]
    public string UsuarioConductorId { get; set; }
    [ForeignKey("UsuarioConductorId")]
    public virtual ApplicationUser Conductor { get; set; }

    // Clave Foránea para el origen
    [Required]
    public int UbicacionOrigenId { get; set; }
    [ForeignKey("UbicacionOrigenId")]
    public virtual Ubicacion Origen { get; set; }

    // Clave Foránea para el destino
    [Required]
    public int UbicacionDestinoId { get; set; }
    [ForeignKey("UbicacionDestinoId")]
    public virtual Ubicacion Destino { get; set; }

    // Un transporte puede tener muchas solicitudes
    public virtual ICollection<SolicitudTransporte> Solicitudes { get; set; }
}