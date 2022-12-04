using Domain.Abstraction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Sqlite.Repository;

public class SqliteClauseRepository : IClauseRepository
{
    private readonly ExpertSystemDbContext _context;

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

    public async Task<IEnumerable<ClauseEntity>> GetAll()
    {
        return await _context.Antecedents.ToListAsync();
    }
}