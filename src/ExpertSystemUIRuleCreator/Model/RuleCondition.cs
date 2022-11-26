using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.Model;

public class RuleCondition : ViewBase
{
    public string Variable { get; set; } = string.Empty;

    public string Condition { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;
}