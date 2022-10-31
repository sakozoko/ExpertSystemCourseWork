using System;
using System.Linq;
using ExpertSystem.Models;
using ExpertSystemUIRuleCreator.Model;

namespace ExpertSystemUIRuleCreator.Extension;

public static class Mapper
{
    public static JsonRule ToJsonRule(this RuleModel model)
    {
        var validation = ValidateMapToRule(model);
        if (!validation.Validated)
            throw new ArgumentException("Property is null or empty", validation.ProblemPropertyName);
        var resultRule = new JsonRule { Name = model.Name };
        foreach (var modelCondition in model.Conditions)
            resultRule.Antecedent.Add(new JsonClause
            {
                Condition = modelCondition.Condition!,
                Value = modelCondition.Value,
                Variable = modelCondition.Variable
            });

        resultRule.Consequent = new JsonClause
        {
            Condition = model.Result.Condition!,
            Value = model.Result.Value,
            Variable = model.Result.Variable
        };
        return resultRule;
    }

    private static (bool Validated, string ProblemPropertyName) ValidateMapToRule(RuleModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
            return (false, nameof(model.Name));
        if (string.IsNullOrWhiteSpace(model.Result.Variable))
            return (false, nameof(model.Result.Variable));
        if (string.IsNullOrWhiteSpace(model.Result.Value))
            return (false, nameof(model.Result.Value));
        if (string.IsNullOrWhiteSpace(model.Result.Condition))
            return (false, nameof(model.Result.Condition));
        if (model.Conditions.Count == 0 ||
            model.Conditions.Any(condition =>
                string.IsNullOrWhiteSpace(condition.Variable) || string.IsNullOrWhiteSpace(condition.Value)
                                                              || string.IsNullOrWhiteSpace(condition.Condition)))
            return (false, nameof(model.Conditions));
        return (true, string.Empty);
    }

    public static bool CanMapToRule(this RuleModel model)
    {
        return ValidateMapToRule(model).Validated;
    }
}