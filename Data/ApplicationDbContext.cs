using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RuedaYPatas.Models;

namespace RuedaYPatas.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ubicacion> Ubicaciones { get; set; }
    public DbSet<Mascota> Mascotas { get; set; }
    public DbSet<Hospedaje> Hospedajes { get; set; }
    public DbSet<Transporte> Transportes { get; set; }
    public DbSet<ReservaHospedaje> ReservasHospedaje { get; set; }
    public DbSet<SolicitudTransporte> SolicitudesTransporte { get; set; }
    public DbSet<Comentario> Comentarios { get; set; }
    public DbSet<Raza> Razas { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Reglas para Comentarios
        builder.Entity<Comentario>()
            .HasOne(c => c.Autor)
            .WithMany(u => u.ComentariosEnviados)
            .HasForeignKey(c => c.AutorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Comentario>()
            .HasOne(c => c.Receptor)
            .WithMany(u => u.ComentariosRecibidos)
            .HasForeignKey(c => c.ReceptorId)
            .OnDelete(DeleteBehavior.NoAction);

        // Reglas para Transporte
        builder.Entity<Transporte>()
            .HasOne(t => t.Origen)
            .WithMany()
            .HasForeignKey(t => t.UbicacionOrigenId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Transporte>()
            .HasOne(t => t.Destino)
            .WithMany()
            .HasForeignKey(t => t.UbicacionDestinoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<ReservaHospedaje>()
            .HasOne(r => r.Mascota)
            .WithMany()
            .HasForeignKey(r => r.MascotaId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<SolicitudTransporte>()
            .HasOne(s => s.Mascota)
            .WithMany()
            .HasForeignKey(s => s.MascotaId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}