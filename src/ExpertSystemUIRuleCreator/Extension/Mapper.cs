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
        var resultRule = new RuleEntity { Id=model.Id,Name = model.Name };
        foreach (var modelCondition in model.Conditions)
            resultRule.Antecedents.Add(new Antecedent()
            {
                Id = modelCondition.Id,
                Condition = modelCondition.Condition,
                Value = modelCondition.Value,
                Name = modelCondition.Variable,
                RuleId = model.Id
            });

        resultRule.Conclusion = new Conclusion()
        {
            Id=model.Result.Id,
            Condition = model.Result.Condition,
            Value = model.Result.Value,
            Name = model.Result.Variable
        };
        resultRule.ConclusionId= resultRule.Conclusion.Id;
        return resultRule;
    }

    public static RuleModel ToRuleModel(this RuleEntity ruleEntity)
    {
        var model = new RuleModel {Id=ruleEntity.Id, Name = ruleEntity.Name };
        foreach (var clauseEntity in ruleEntity.Antecedents)
            model.Conditions.Add(new RuleConditionModel
            {
                Id = clauseEntity.Id,
                Condition = clauseEntity.Condition,
                Value = clauseEntity.Value,
                Variable = clauseEntity.Name
            });
        model.Result.Id=ruleEntity.Conclusion.Id;
        model.Result.Condition = ruleEntity.Conclusion.Condition;
        model.Result.Value = ruleEntity.Conclusion.Value;
        model.Result.Variable = ruleEntity.Conclusion.Name;
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