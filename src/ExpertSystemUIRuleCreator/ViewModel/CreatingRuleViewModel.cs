using System.Linq;
using System.Windows.Input;
using ExpertSystemUIRuleCreator.Extension;
using ExpertSystemUIRuleCreator.Model;
using ExpertSystemUIRuleCreator.Service;
using WPFBase.Command;
using WPFBase.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class CreatingRuleViewModel : ViewBase
{
    private readonly RulesManager _rulesManager;
    private RuleModel _rule;


    public CreatingRuleViewModel(RulesManager rulesManager)
    {
        _rule = new RuleModel();
        _rulesManager = rulesManager;
        ModificationRuleViewModel = new ModificationRuleViewModel(_rule);
        SaveRuleCommand = new LambdaCommand(ExecuteSavingRule, CanExecuteSavingRule);
        ClearRuleValuesCommand =
            new LambdaCommand(ExecuteClearingRuleValuesCommand, CanExecuteClearingRuleValuesCommand);
    }

    public ModificationRuleViewModel ModificationRuleViewModel { get; }

    public RuleModel Rule
    {
        get => _rule;
        set => SetField(ref _rule, value);
    }

    public ICommand ClearRuleValuesCommand { get; }
    public ICommand SaveRuleCommand { get; }

    private bool CanExecuteClearingRuleValuesCommand(object? parameter)
    {
        if (parameter is RuleModel model)
            return !string.IsNullOrWhiteSpace(model.Name)
                   | AnyPropertyOfRuleConditionHasValue(model.Result)
                   | model.Conditions.Any();

        return false;
    }

    private void ExecuteClearingRuleValuesCommand(object? parameter)
    {
        if (parameter is IClearable model) model.Clear();
    }

    private static bool AnyPropertyOfRuleConditionHasValue(RuleConditionModel c)
    {
        return !string.IsNullOrWhiteSpace(c.Variable)
               || !string.IsNullOrWhiteSpace(c.Value)
               || !string.IsNullOrWhiteSpace(c.Condition);
    }

    private bool CanExecuteSavingRule(object? parameter)
    {
        return parameter is RuleModel model && model.CanMapToRule();
    }

    private void ExecuteSavingRule(object? parameter)
    {
        _rulesManager.Add(Rule);
        Rule = new RuleModel();
        ModificationRuleViewModel.Rule = Rule;
    }
}