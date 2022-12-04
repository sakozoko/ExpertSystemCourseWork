using Domain.Abstraction;
using Domain.Entities;

namespace Infrastructure.Json.Repository;

public class ClauseRepositoryJson : IClauseRepository
{
    private readonly IEnumerable<RuleEntity> _rules;

    public ClauseRepositoryJson(IEnumerable<RuleEntity> rules)
    {
        _rules = rules;
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
        return await Task.FromResult(_rules.SelectMany(c => c.Antecedents).DistinctBy(c => c.Name));
    }
}