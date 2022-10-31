using ExpertSystemUI.ViewModel.Base;

namespace ExpertSystemUI.Model;

public class Fact : ViewBase
{
    private string? _condition;

    private string? _value;
    private string? _variable;

    public string? Variable
    {
        get => _variable;
        set => SetField(ref _variable, value);
    }

    public string? Value
    {
        get => _value;
        set => SetField(ref _value, value);
    }

    public string? Condition
    {
        get => _condition;
        set => SetField(ref _condition, value);
    }
}