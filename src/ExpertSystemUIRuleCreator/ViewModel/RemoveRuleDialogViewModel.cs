using ExpertSystemUIRuleCreator.Model;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class RemoveRuleDialogViewModel : ViewBase
{
    public RuleModel Rule { get; }
    
    public RemoveRuleDialogViewModel(RuleModel rule)
    {
        Rule = rule;
    }
    public RemoveRuleDialogViewModel(): this(new RuleModel())
    {
    }
}