using chen0040.ExpertSystem;
using Domain.Abstraction;
using ExpertSystemClientService.Extension;

namespace ExpertSystemClientService;

public class RuleInferenceEngineFacade : RuleInferenceEngine
{
    private readonly IRuleRepository _ruleRepository;
    private readonly IClauseRepository _clauseRepository;
    public RuleInferenceEngineFacade(IRuleRepository ruleRepository, IClauseRepository clauseRepository)
    {
        _ruleRepository = ruleRepository;
        _clauseRepository = clauseRepository;
        Task.Run(async ()=> await SetKnowledgeBase());
    }
    
    public async Task<IEnumerable<string>> GetClauseNames() => 
        (await _clauseRepository.GetAll()).Where(c=>!string.IsNullOrEmpty(c.Name)).Select(c => c.Name);


    public void SetFacts(IEnumerable<(string Variable, string Condition, string Value)> facts)
    {
        ClearFacts();
        foreach (var clause in facts.Select(c=>c.MapTupleClauseToClause()))
        {
            AddFact(clause);
        }
    }

    public string GetResult(string variable)
    {
        var list = new List<Clause>();
        var result = Infer(variable, list);
        return result?.ToString() ?? "Rule is not found";
    }
    
    private async Task SetKnowledgeBase()
    {
        foreach (var ruleEntity in await _ruleRepository.GetAll())
        {
            AddRule(ruleEntity.MapRuleEntityToRule());
        }
    }

}
