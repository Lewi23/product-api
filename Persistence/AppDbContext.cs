using Microsoft.EntityFrameworkCore;
using product_api.Entities;

namespace product_api.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .ToTable("Products");

        modelBuilder.Entity<Product>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Product>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Product>()
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Product>()
            .Property(x => x.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .Property(x => x.Stock)
            .IsRequired();
    }
}