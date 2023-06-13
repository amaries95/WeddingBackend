using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Appllication.Data;

public class WeddingContext : DbContext
{
    public WeddingContext(DbContextOptions<WeddingContext> options) : base(options)
    {
        
    }

    public DbSet<Guest> Guests { get; set; }

    public DbSet<View> Views { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guest>().ToTable("Guests");
        modelBuilder.Entity<View>().ToTable("Views");
    }
}
