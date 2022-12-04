using chen0040.ExpertSystem;
using Domain.Entities;

namespace ExpertSystemClientService.Extension;

public static class Mapper
{
    public static Rule MapRuleEntityToRule(this RuleEntity ruleEntity)
    {
        var rule = new Rule(ruleEntity.Name);
        foreach (var clause in ruleEntity.Antecedents) rule.AddAntecedent(MapClauseEntityToClause(clause));
        rule.setConsequent(MapClauseEntityToClause(ruleEntity.Conclusion));
        return rule;
    }

    public static Clause? MapClauseEntityToClause(this ClauseEntity jsonClause)
    {
        return (jsonClause.Name, jsonClause.Condition, jsonClause.Value).MapTupleClauseToClause();
    }

    public static Clause? MapTupleClauseToClause(this (string Variable, string Condition, string Value) jsonClause)
    {
        return jsonClause.Condition switch
        {
            "=" => new IsClause(jsonClause.Variable, jsonClause.Value),
            "<" => new LessClause(jsonClause.Variable, jsonClause.Value),
            ">" => new GreaterClause(jsonClause.Variable, jsonClause.Value),
            _ => null
        };
    }
}