using System;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.Model;

public class RuleConditionModel : ViewBase, ICloneable, IClearable
{
    public int Id { get; set; }
    private string _condition = string.Empty;
    private string _value = string.Empty;
    private string _variable = string.Empty;

    public string Variable
    {
        get => _variable;
        set => SetField(ref _variable, value);
    }

    public string Condition
    {
        get => _condition;
        set => SetField(ref _condition, value);
    }

    public string Value
    {
        get => _value;
        set => SetField(ref _value, value);
    }

    //implement IClearable
    public void Clear()
    {
        Variable = string.Empty;
        Condition = string.Empty;
        Value = string.Empty;
    }

    //implement ICloneable
    public object Clone()
    {
        return new RuleConditionModel
        {
            Id=Id,
            Variable = Variable,
            Condition = Condition,
            Value = Value
        };
    }
}