using ExpertSystemUIRuleCreator.Extension;
using ExpertSystemUIRuleCreator.Interfaces;
using ExpertSystemUIRuleCreator.Model;

namespace ExpertSystemUIRuleCreator.Service;

public class RuleSavingService
{
    public RuleSavingService(IRuleSource ruleSource)
    {
        RuleSource = ruleSource;
    }

    private IRuleSource RuleSource { get; }

    public void Save(RuleModel model)
    {
        var rule = model.ToJsonRule();
        RuleSource.Add(rule);
    }

    public void Save(RuleModel[] model)
    {
        foreach (var ruleModel in model) Save(ruleModel);
    }
}