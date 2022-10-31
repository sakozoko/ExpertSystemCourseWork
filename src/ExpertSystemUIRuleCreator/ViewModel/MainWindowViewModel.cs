using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ExpertSystemUIRuleCreator.Command;
using ExpertSystemUIRuleCreator.Extension;
using ExpertSystemUIRuleCreator.Model;
using ExpertSystemUIRuleCreator.Service;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class MainWindowViewModel : ViewBase
{
    private readonly RuleSavingService _savingService;
    private RuleModel _rule;

    public MainWindowViewModel()
    {
        PossibleConditions = new[] { "=", "<", ">" };

        _rule = new RuleModel();

        Rules = new ObservableCollection<RuleModel>();

        var ruleSource = new JsonRuleSource("knowledgeBase.json");

        _savingService = new RuleSavingService(ruleSource);

        #region Commands

        AddConditionCommand = new LambdaCommand(ExecuteAddingCondition);
        RemoveConditionCommand = new LambdaCommand(ExecuteRemovingCondition, CanExecuteRemovingCondition);
        SaveRuleCommand = new LambdaCommand(ExecuteSavingRule, CanExecuteSavingRule);
        RemoveRuleCommand = new LambdaCommand(ExecuteRemovingRule);

        #endregion
    }

    public RuleModel Rule
    {
        get => _rule;
        private set => SetField(ref _rule, value);
    }

    public ObservableCollection<RuleModel> Rules { get; }

    public IEnumerable<string> PossibleConditions { get; }

    private void ExecuteRemovingRule(object? parameter)
    {
        if (parameter is RuleModel rule) Rules.Remove(rule);
    }

    private void ExecuteAddingCondition(object? parameter)
    {
        Rule.Conditions.Add(new RuleCondition());
    }

    private void ExecuteRemovingCondition(object? parameter)
    {
        Rule.Conditions.Remove((parameter as RuleCondition)!);
    }

    private bool CanExecuteRemovingCondition(object? parameter)
    {
        return parameter is RuleCondition;
    }

    private bool CanExecuteSavingRule(object? parameter)
    {
        return parameter is RuleModel model && model.CanMapToRule();
    }

    private void ExecuteSavingRule(object? parameter)
    {
        _savingService.Save(Rule);
        Rules.Add(Rule);
        Rule = new RuleModel();
    }

    #region Commands

    public ICommand AddConditionCommand { get; }

    public ICommand RemoveConditionCommand { get; }

    public ICommand SaveRuleCommand { get; }

    public ICommand RemoveRuleCommand { get; }

    #endregion
}