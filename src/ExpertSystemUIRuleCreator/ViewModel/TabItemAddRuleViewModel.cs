using System;
using System.Collections.Generic;
using System.Windows.Input;
using ExpertSystemUIRuleCreator.Command;
using ExpertSystemUIRuleCreator.Extension;
using ExpertSystemUIRuleCreator.Model;
using ExpertSystemUIRuleCreator.Service;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class TabItemAddRuleViewModel :ViewBase
{
    private RuleModel _rule;
    private readonly RuleSavingService _savingService;
    public IEnumerable<string> PossibleConditions { get; }
    public RuleModel Rule
    {
        get => _rule;
        private set => SetField(ref _rule, value);
    }

    public TabItemAddRuleViewModel(RuleSavingService savingService, IEnumerable<string> possibleConditions)
    {
        _rule = new RuleModel();
        _savingService = savingService;
        PossibleConditions = possibleConditions;
        
        RemoveConditionCommand = new LambdaCommand(ExecuteRemovingCondition, CanExecuteRemovingCondition);
        AddConditionCommand = new LambdaCommand(ExecuteAddingCondition);
        
        SaveRuleCommand = new LambdaCommand(ExecuteSavingRule, CanExecuteSavingRule);   
    }
    private void ExecuteAddingCondition(object? parameter)
    {
        Rule.Conditions.Add(new RuleCondition());
    }



    private bool CanExecuteSavingRule(object? parameter)
    {
        return parameter is RuleModel model && model.CanMapToRule();
    }

    private void ExecuteSavingRule(object? parameter)
    {
        _savingService.Save(Rule);
        Rule = new RuleModel();
    }

    private void ExecuteRemovingCondition(object? parameter)
    {
        Rule.Conditions.Remove((parameter as RuleCondition)!);
    }

    private bool CanExecuteRemovingCondition(object? parameter)
    {
        return parameter is RuleCondition;
    }
    public ICommand RemoveConditionCommand { get; }
    
    public ICommand AddConditionCommand { get; }



    public ICommand SaveRuleCommand { get; }
}