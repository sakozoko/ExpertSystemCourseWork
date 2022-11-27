using System.Windows.Input;
using ExpertSystemUIRuleCreator.Command;
using ExpertSystemUIRuleCreator.Model;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class ModificationRuleViewModel : ViewBase
{
    private RuleModel _rule;

    public ModificationRuleViewModel(RuleModel rule)
    {
        _rule = rule;
        RemoveConditionCommand = new LambdaCommand(ExecuteRemovingCondition, CanExecuteRemovingCondition);
        AddConditionCommand = new LambdaCommand(ExecuteAddingCondition);
    }

    public RuleModel Rule
    {
        get => _rule;
        set => SetField(ref _rule, value);
    }

    public ICommand RemoveConditionCommand { get; }

    public ICommand AddConditionCommand { get; }

    private void ExecuteAddingCondition(object? parameter)
    {
        Rule.Conditions.Add(new RuleConditionModel());
    }

    private void ExecuteRemovingCondition(object? parameter)
    {
        Rule.Conditions.Remove((parameter as RuleConditionModel)!);
    }

    private bool CanExecuteRemovingCondition(object? parameter)
    {
        return parameter is RuleConditionModel;
    }
}