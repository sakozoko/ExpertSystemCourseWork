using Domain.Abstraction;
using Infrastructure.Json;
using Infrastructure.Sqlite.Repository;

namespace Infrastructure.Sqlite;

public class SqliteRepositoryFactory : IRepositoryFactory
{
    private readonly ExpertSystemDbContext _context;

    public SqliteRepositoryFactory(ExpertSystemDbContext dbContext)
    {
        _context = dbContext;
    }
    
    public IClauseRepository CreateClauseRepository()
    {
        return new SqliteClauseRepository(_context);
    }

    public IRuleRepository CreateRuleRepository()
    {
        return new SqliteRuleRepository(_context);
    }
}