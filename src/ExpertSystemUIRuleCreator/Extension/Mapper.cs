using System;
using System.Linq;
using Domain.Entities;
using ExpertSystemUIRuleCreator.Model;

namespace ExpertSystemUIRuleCreator.Extension;

public static class Mapper
{
    public static RuleEntity ToRuleEntity(this RuleModel model)
    {
        var validation = ValidateMapToRule(model);
        if (!validation.Validated)
            throw new ArgumentException("Property is null or empty", validation.ProblemPropertyName);
        var resultRule = new RuleEntity { Name = model.Name };
        foreach (var modelCondition in model.Conditions)
            resultRule.Antecedent.Add(new ClauseEntity
            {
                Condition = modelCondition.Condition,
                Value = modelCondition.Value,
                Variable = modelCondition.Variable
            });

        resultRule.Consequent = new ClauseEntity
        {
            Condition = model.Result.Condition,
            Value = model.Result.Value,
            Variable = model.Result.Variable
        };
        return resultRule;
    }

    public static RuleModel ToRuleModel(this RuleEntity ruleEntity)
    {
        var model = new RuleModel { Name = ruleEntity.Name };
        foreach (var jsonClause in ruleEntity.Antecedent)
            model.Conditions.Add(new RuleConditionModel
            {
                Condition = jsonClause.Condition,
                Value = jsonClause.Value,
                Variable = jsonClause.Variable
            });

        model.Result.Condition = ruleEntity.Consequent.Condition;
        model.Result.Value = ruleEntity.Consequent.Value;
        model.Result.Variable = ruleEntity.Consequent.Variable;

        return model;
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