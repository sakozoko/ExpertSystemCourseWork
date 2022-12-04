using Domain.Abstraction;
using Domain.Entities;
using Infrastructure.Json;

namespace Infrastructure.Sqlite.Repository;

public class SqliteClauseRepository : IClauseRepository
{
    private ExpertSystemDbContext _context;
    public SqliteClauseRepository(ExpertSystemDbContext context)
    {
        _context = context;
    }
    public Task Add(ClauseEntity rule)
    {
        throw new NotImplementedException();
    }

    public Task Delete(ClauseEntity rule)
    {
        throw new NotImplementedException();
    }

    public Task Update(ClauseEntity rule)
    {
        throw new NotImplementedException();
    }

    public Task<ClauseEntity> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ClauseEntity>> GetAll()
    {
        throw new NotImplementedException();
    }
}