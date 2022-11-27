using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task<bool> Update(RuleModel model)
    {
        var foundRule = Rules.FirstOrDefault(c => c.Name == model.Name);
        if (foundRule == null)
            return false;
        Rules[Rules.IndexOf(foundRule)] = model;
        var rule = model.ToRuleEntity();
        await RuleRepository.Update(rule);
        return true;
    }

    public async Task<bool> Remove(RuleModel model)
    {
        var foundRule = Rules.FirstOrDefault(c => c.Name == model.Name);
        if (foundRule == null) return false;

        var rule = model.ToRuleEntity();
        await RuleRepository.Delete(rule);
        return Rules.Remove(foundRule);
    }
}