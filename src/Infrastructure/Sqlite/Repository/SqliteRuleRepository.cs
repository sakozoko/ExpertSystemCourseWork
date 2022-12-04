using Domain.Abstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Sqlite.Repository;

public class SqliteRuleRepository : IRuleRepository
{
    private readonly ExpertSystemDbContext _context;

    public SqliteRuleRepository(ExpertSystemDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task Add(RuleEntity rule)
    {
        await _context.Rules.AddAsync(rule);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task Delete(RuleEntity rule)
    {
        _context.Rules.Remove(rule);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task Update(RuleEntity rule)
    {
        _context.Rules.Update(rule);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<RuleEntity?> Get(int id)
    {
        return await _context.Rules
            .Include(c => c.Antecedents)
            .Include(c => c.Conclusion)
            .FirstAsync(c => c.Id == id)
            .ConfigureAwait(false);
    }

    public async Task<IEnumerable<RuleEntity>> GetAll()
    {
        return await _context.Rules
            .Include(c => c.Antecedents)
            .Include(c => c.Conclusion)
            .ToListAsync()
            .ConfigureAwait(false);
    }
}