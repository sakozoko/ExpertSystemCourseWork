using chen0040.ExpertSystem;
using ExpertSystem.Models;

namespace ExpertSystem.Extension;

public static class Mapper
{
    public static Rule MapJsonRuleToRule(this JsonRule jsonRule)
    {
        var rule = new Rule(jsonRule.Name);
        foreach (var clause in jsonRule.Antecedent) rule.AddAntecedent(MapJsonClauseToClause(clause));
        rule.setConsequent(MapJsonClauseToClause(jsonRule.Consequent));
        return rule;
    }

    public static Clause? MapJsonClauseToClause(this JsonClause? jsonClause)
    {
        if (jsonClause is null)
            return null;
        return (jsonClause.Variable, jsonClause.Condition, jsonClause.Value).MapTupleClauseToClause();
    }

    public static Clause? MapTupleClauseToClause(this (string? Variable, string? Condition, string? Value) jsonClause)
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