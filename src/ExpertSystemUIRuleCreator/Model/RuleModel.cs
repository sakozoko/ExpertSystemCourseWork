using System.Collections.ObjectModel;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.Model;

public class RuleModel : ViewBase
{
    public RuleModel() : this(string.Empty)
    {
    }

    private RuleModel(string name)
    {
        Name = name;
        Conditions = new ObservableCollection<RuleCondition>();
        Result = new RuleCondition { Condition = "=" };
    }

    public string? Name { get; set; }
    public ObservableCollection<RuleCondition> Conditions { get; }

    public RuleCondition Result { get; }
}