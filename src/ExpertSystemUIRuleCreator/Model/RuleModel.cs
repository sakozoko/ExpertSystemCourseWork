using System;
using System.Collections.ObjectModel;
using WPFBase.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.Model;

public class RuleModel : ViewBase, ICloneable, IClearable
{
    public int Id { get; set; }
    private string _name;
    private RuleConditionModel _result;

    public RuleModel() : this(0,string.Empty)
    {
    }

    private RuleModel(int id,string name)
    {
        Id = id;
        _name = name;
        Conditions = new ObservableCollection<RuleConditionModel>();
        _result = new RuleConditionModel();
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

    public void Clear()
    {
        Name = string.Empty;
        Conditions.Clear();
        Result.Clear();
    }

    //implement deep copy manually created new instance of RuleModel and its properties
    public object Clone()
    {
        var rule = new RuleModel(Id,Name);
        foreach (var condition in Conditions) rule.Conditions.Add((RuleConditionModel)condition.Clone());
        rule.Result = (RuleConditionModel)Result.Clone();
        return rule;
    }


}