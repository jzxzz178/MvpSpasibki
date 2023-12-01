using Microsoft.EntityFrameworkCore;

namespace MvpSpasibki.Db;

public sealed class SpasibkiContext : DbContext
{
    private const string DbName = "Spasibki.sqlite";
    public DbSet<Spasibka> Spasibki;

    public SpasibkiContext()
    {
        
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbName}");
    }
}