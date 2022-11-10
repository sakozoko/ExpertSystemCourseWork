using System.Collections.ObjectModel;
using System.Linq;
using ExpertSystemUIRuleCreator.Extension;
using ExpertSystemUIRuleCreator.Interfaces;
using ExpertSystemUIRuleCreator.Model;

namespace ExpertSystemUIRuleCreator.Service;

public class RuleSavingService
{
    public ObservableCollection<RuleModel> Rules { get; }

    public RuleSavingService(IRuleSource ruleSource)
    {
        RuleSource = ruleSource;

        Rules = new ObservableCollection<RuleModel>(RuleSource.GetAll()
            .Result
            .Select(c => c.ToRuleModel()));

    }

    private IRuleSource RuleSource { get; }

    public void Save(RuleModel model)
    {
        var rule = model.ToJsonRule();
        RuleSource.Add(rule);
        Rules.Add(model);
    }
    public void Remove(RuleModel model)
    {
        var rule = model.ToJsonRule();
        RuleSource.Remove(rule);
        Rules.Remove(model);
    }

    public void Save(RuleModel[] model)
    {
        foreach (var ruleModel in model) Save(ruleModel);
    }
}