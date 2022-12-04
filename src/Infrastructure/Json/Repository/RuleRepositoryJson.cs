using System.Text.Json;
using Domain.Abstraction;
using Domain.Entities;

namespace Infrastructure.Json.Repository;

public class RuleRepositoryJson : IRuleRepository
{
    private readonly string _path;

    public RuleRepositoryJson(string path)
    {
        _path = path;
    }

    public async Task Add(RuleEntity rule)
    {
        var rules = await GetAll();
        await PrependAndRewriteDataFile(rules, rule);
    }

    public async Task Delete(RuleEntity rule)
    {
        var resultingTuple = await CanDeletionLocally(rule);
        if (resultingTuple.deletingRule is not null)
        {
            resultingTuple.cachedList.Remove(resultingTuple.deletingRule);
            await RewriteDataFile(resultingTuple.cachedList);
        }
    }

    public async Task Update(RuleEntity rule)
    {
        var resultingTuple = await CanDeletionLocally(rule);
        if (resultingTuple.deletingRule is not null)
        {
            var indexOfRemovingRule = resultingTuple.cachedList.IndexOf(resultingTuple.deletingRule);
            resultingTuple.cachedList[indexOfRemovingRule] = rule;
            await PrependAndRewriteDataFile(resultingTuple.cachedList, rule);
        }
    }
    private async Task PrependAndRewriteDataFile(IEnumerable<RuleEntity> rules, RuleEntity rule)
    {
        var rulesWithNewRule = rules.Prepend(rule);
        await RewriteDataFile(rulesWithNewRule);
    }
    
    private async Task RewriteDataFile(IEnumerable<RuleEntity> rules)
    {
        await using var sw = new StreamWriter(_path).BaseStream;
        await JsonSerializer.SerializeAsync(sw, rules);
    }

    private async Task<(RuleEntity? deletingRule, IList<RuleEntity> cachedList)> CanDeletionLocally(RuleEntity removingRule)
    {
        var rules = await GetAll();
        var list = rules.ToList();
        var foundRuleEntity = list.FirstOrDefault(c => c.Name?.Equals(removingRule.Name) ?? false);
        return (foundRuleEntity, list);
    }

    public Task<RuleEntity> Get(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<RuleEntity>> GetAll()
    {
        await using var sr = new StreamReader(_path).BaseStream;
        var rules = await JsonSerializer.DeserializeAsync<RuleEntity[]>(sr).ConfigureAwait(false) ?? Array.Empty<RuleEntity>();
        return rules;
    }
}