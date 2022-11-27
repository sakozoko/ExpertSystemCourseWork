using System.Collections.ObjectModel;
using System.Linq;
using Domain.Abstraction;
using ExpertSystemUIRuleCreator.Extension;
using ExpertSystemUIRuleCreator.Model;

namespace ExpertSystemUIRuleCreator.Service;

public class RulesManager
{
    public RulesManager(IRuleRepository ruleRepository)
    {
        RuleRepository = ruleRepository;

        Rules = new ObservableCollection<RuleModel>(RuleRepository.GetAll()
            .Result
            .Select(c => c.ToRuleModel()));
    }

    public ObservableCollection<RuleModel> Rules { get; }

    private IRuleRepository RuleRepository { get; }

    public void Add(RuleModel model)
    {
        var rule = model.ToRuleEntity();
        RuleRepository.Add(rule);
        Rules.Add(model);
    }

    public bool Update(RuleModel model)
    {
        if (!Remove(model)) return false;
        
        Add(model);
        return true;

    }

    public bool Remove(RuleModel model)
    {
        var rule = model.ToRuleEntity();
        RuleRepository.Delete(rule);
        var foundRule = Rules.FirstOrDefault(c => c.Name == model.Name);
        return foundRule != null && Rules.Remove(foundRule);
    }

}