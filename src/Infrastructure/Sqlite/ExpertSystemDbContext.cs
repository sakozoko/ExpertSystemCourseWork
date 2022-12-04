using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Sqlite;

public class ExpertSystemDbContext : DbContext
{
    public DbSet<RuleEntity> Rules { get; set; }

    public DbSet<Conclusion> Conclusions { get; set; }
    public DbSet<Antecedent> Conditions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=Db.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RuleEntity>(entity =>
        {
            entity.HasMany(e => e.Antecedents)
                .WithOne().HasForeignKey(c=>c.RuleId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(c=>c.Conclusion)
                .WithOne().HasForeignKey<RuleEntity>(c=>c.ConclusionId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(c => c.Name).HasMaxLength(50);
        });
        modelBuilder.Entity<ClauseEntity>(e =>
        {
            e.Property(c => c.Condition).HasMaxLength(2);
            e.Property(c => c.Name).HasMaxLength(50);
            e.Property(c => c.Value).HasMaxLength(50);
        });
    }
}