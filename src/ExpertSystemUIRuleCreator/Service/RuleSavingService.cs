using System.Collections.ObjectModel;
using System.Linq;
using Domain.Abstraction;
using ExpertSystemUIRuleCreator.Extension;
using ExpertSystemUIRuleCreator.Model;

namespace ExpertSystemUIRuleCreator.Service;

public class RuleSavingService
{
    public RuleSavingService(IRuleRepository ruleRepository)
    {
        RuleRepository = ruleRepository;

        Rules = new ObservableCollection<RuleModel>(RuleRepository.GetAll()
            .Result
            .Select(c => c.ToRuleModel()));
    }

    public ObservableCollection<RuleModel> Rules { get; }

    private IRuleRepository RuleRepository { get; }

    public void Save(RuleModel model)
    {
        var rule = model.ToRuleEntity();
        RuleRepository.Add(rule);
        Rules.Add(model);
    }

    public void Remove(RuleModel model)
    {
        var rule = model.ToRuleEntity();
        RuleRepository.Delete(rule);
        Rules.Remove(model);
    }

}