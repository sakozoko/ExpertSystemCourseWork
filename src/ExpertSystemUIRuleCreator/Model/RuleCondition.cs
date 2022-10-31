using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.Model;

public class RuleCondition : ViewBase
{
    public string? Variable { get; set; }

    public string? Condition { get; set; }

    public string? Value { get; set; }
}