using System;
using System.Collections.ObjectModel;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.Model;

public class RuleModel : ViewBase, ICloneable, IClearable
{
    private string _name;
    private RuleConditionModel _result;

    public RuleModel() : this(string.Empty)
    {
    }

    private RuleModel(string name)
    {
        _name = name;
        Conditions = new ObservableCollection<RuleConditionModel>();
        _result = new RuleConditionModel ();
    }

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public ObservableCollection<RuleConditionModel> Conditions { get; }

    public RuleConditionModel Result
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
            rule.Conditions.Add((RuleConditionModel)condition.Clone());
        }
        rule.Result = (RuleConditionModel)Result.Clone();
        return rule;
    }

    public void Clear()
    {
        Name=string.Empty;
        Conditions.Clear();
        Result.Clear();
    }
}