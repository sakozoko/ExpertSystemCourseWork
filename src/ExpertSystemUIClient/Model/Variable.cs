using System;
using ExpertSystemUI.Extension;
using ExpertSystemUI.ViewModel.Base;

namespace ExpertSystemUI.Model;

public class Variable : ViewBase, ICloneable
{
    private Condition? _condition;
    private string? _inputValue;
    private string? _name;

    public string? Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string? InputValue
    {
        get => _inputValue;
        set => SetField(ref _inputValue, value);
    }

    public Condition? Condition
    {
        get => _condition;
        set => SetField(ref _condition, value);
    }

    public void Deconstruct(out string? variable, out string? condition, out string? value)
    {
        variable = Name;
        condition = Condition.MapToString();
        value = InputValue;
    }

    public (string?, string?, string?) Deconstruct()
    {
        return (_name, _condition.MapToString(), _inputValue);
    }

    public object Clone()
    {
        //return new cloneable variable
        return new Variable
        {
            Name = Name,
            Condition = Condition,
            InputValue = InputValue
        };
    }
}