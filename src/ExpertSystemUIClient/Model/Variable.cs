using ExpertSystemUI.ViewModel.Base;

namespace ExpertSystemUI.Model;

public class Variable : ViewBase
{
    private string? _condition;
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

    public string? Condition
    {
        get => _condition;
        set => SetField(ref _condition, value);
    }

    public void Deconstruct(out string? variable, out string? condition, out string? value)
    {
        variable = Name;
        condition = Condition;
        value = InputValue;
    }

    public (string?, string?, string?) Deconstruct()
    {
        return (_name, _condition, _inputValue);
    }
}