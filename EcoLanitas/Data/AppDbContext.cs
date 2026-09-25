using EcoLanitas.web.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoLanitas.web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Las tablas ya existen en Supabase (Postgres), así que EF no debe
        // intentar crearlas ni gestionarlas con Migrations por ahora.
        // Solo mapeamos lo que ya está ahí.

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
        });
    }
}
