using System;
using System.Collections.ObjectModel;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.Model;

public class RuleModel : ViewBase, ICloneable
{
    private string _name;
    private RuleCondition _result;

    public RuleModel() : this(string.Empty)
    {
    }

    private RuleModel(string name)
    {
        _name = name;
        Conditions = new ObservableCollection<RuleCondition>();
        _result = new RuleCondition ();
    }

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public ObservableCollection<RuleCondition> Conditions { get; }

    public RuleCondition Result
    {
        get => _result;
        set => SetField(ref _result, value);
    }

    //implement deep copy manually created new instance of RuleModel and its properties
    public object Clone()
    {
        var rule = new RuleModel(Name);
        foreach (var condition in Conditions)
        {
            rule.Conditions.Add((RuleCondition)condition.Clone());
        }
        rule.Result = (RuleCondition)Result.Clone();
        return rule;
    }

}