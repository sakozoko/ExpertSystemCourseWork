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
        await _context.SaveChangesAsync()
            .ConfigureAwait(false);
    }

    public async Task Delete(RuleEntity rule)
    {
        var existingRule = _context.Rules.Local.SingleOrDefault(c=>c.Id== rule.Id);
        _context.Rules
            .Remove(existingRule ?? rule);
        await _context.SaveChangesAsync()
            .ConfigureAwait(false);
    }

    public async Task Update(RuleEntity rule)
    {
        //implement update method for ef core
        var existingRule = _context.Rules.Local.SingleOrDefault(r => r.Id == rule.Id);
        if (existingRule != null)
            _context.Entry(existingRule).State = EntityState.Detached;
        var existingConclusion = _context.Conclusions.Local.SingleOrDefault(c => c.Id == rule.Conclusion.Id);
        if (existingConclusion != null)
        {
            _context.Entry(existingConclusion).State = EntityState.Detached;
            _context.Update(rule.Conclusion);
        }
            
        foreach (var ruleAntecedent in rule.Antecedents)
        {
            var existingAntecedent = _context.Antecedents.Local.SingleOrDefault(a => a.Id == ruleAntecedent.Id);
            if (existingAntecedent != null)
            {
                _context.Entry(existingAntecedent).State = EntityState.Detached;
                _context.Update(existingAntecedent);
            }
        }
        _context.Update(rule);
        await _context.SaveChangesAsync()
            .ConfigureAwait(false);

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