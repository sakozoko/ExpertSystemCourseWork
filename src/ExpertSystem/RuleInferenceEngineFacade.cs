using System.Text.Json;
using chen0040.ExpertSystem;
using ExpertSystem.Extension;
using Domain.Entities;
using Domain.Abstraction;

namespace ExpertSystem;

public class RuleInferenceEngineFacade
{
    private IRuleRepository _ruleRepository;
    private IClauseRepository _clauseRepository;
    public RuleInferenceEngineFacade(IRuleRepository ruleRepository, IClauseRepository clauseRepository)
    {
        _ruleRepository = ruleRepository;
        _clauseRepository = clauseRepository;
        Engine = new RuleInferenceEngine();
        Facts = new List<Clause?>();
        PossibleConditions = new[] { "=", "<", ">" };
        Task.Run(async ()=> await SetKnowledgeBase());
    }

    public RuleInferenceEngine Engine { get; }
    public List<Clause?> Facts { get; }

    public string[] PossibleConditions { get; }
    
    public RuleInferenceEngineFacade SetFacts(IEnumerable<Clause?> clauses)
    {
        foreach (var clause in clauses)
        {
            if (Facts.Select(c => c?.Variable).Contains(clause?.Variable))
            {
                Facts.RemoveAt(
                    Facts.IndexOf(
                        Facts.First(
                            c => c?.Variable.Equals(clause?.Variable) ?? false
                        )
                    )
                );
                var tempArr = new Clause?[Facts.Count];
                Facts.CopyTo(tempArr);
                Facts.Clear();
                Engine.ClearFacts();
                SetFacts(tempArr);
            }

            Engine.AddFact(clause);
            Facts.Add(clause);
        }

        return this;
    }

    public async Task<IEnumerable<string?>> GetClauseNames() => 
        (await _clauseRepository.GetAll()).Select(c => c.Variable);


    public RuleInferenceEngineFacade SetFacts(IEnumerable<(string? Variable, string? Condition, string? Value)> facts)
    {
        return SetFacts(facts
            .Where(x => !string.IsNullOrWhiteSpace(x.Variable) && !string.IsNullOrWhiteSpace(x.Value) &&
                        !string.IsNullOrWhiteSpace(x.Value))
            .Select(c => (c.Variable, c.Condition, c.Value).MapTupleClauseToClause()));
    }

    public string GetResult(string variable)
    {
        var list = new List<Clause>();
        var result = Engine.Infer(variable, list);
        return result?.ToString() ?? "Rule is not found";
    }

    public RuleInferenceEngineFacade ClearFacts()
    {
        Engine.ClearFacts();
        Facts.Clear();
        return this;
    }
    

    
    private async Task SetKnowledgeBase()
    {
        foreach (var ruleEntity in await _ruleRepository.GetAll())
        {
            Engine.AddRule(ruleEntity.MapRuleEntityToRule());
        }
    }
}