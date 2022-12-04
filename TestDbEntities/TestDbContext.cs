using DbDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestDbEntities;

public class TestDbContext : DbContext
{
    public DbSet<Rule> Rules { get; set; }

    public DbSet<Conclusion> Conclusions { get; set; }
    public DbSet<Condition> Conditions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=TestDb.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rule>(entity =>
        {
            entity.HasMany(e => e.Conditions)
                .WithOne().HasForeignKey(c=>c.RuleId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(c=>c.Conclusion)
                .WithOne().HasForeignKey<Rule>(c=>c.ConclusionId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(c => c.Name).HasMaxLength(50);
        });
        modelBuilder.Entity<Clause>(e =>
        {
            e.Property(c => c.Condition).HasMaxLength(2);
            e.Property(c => c.Name).HasMaxLength(50);
            e.Property(c => c.Value).HasMaxLength(50);
        });
    }
}