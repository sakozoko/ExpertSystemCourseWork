using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ExpertSystemUI.Command;
using ExpertSystemUI.Model;
using ExpertSystemUI.ViewModel.Base;

namespace ExpertSystemUI.ViewModel.Control;

public class CreatingFactViewViewModel :ViewBase
{
    private readonly ObservableCollection<Variable> _possibleVariables;
    public ObservableCollection<Variable> PossibleVariables { get=>_possibleVariables; 
        init => SetField(ref _possibleVariables, value); }
    
    public Action<IEnumerable<Variable>>? OnFactCreated { get; init; }
    public CreatingFactViewViewModel()
    {
        SettingFactCommand = new LambdaCommand(ExecuteSettingFactCommand, CanExecuteSettingFactCommand);
        _possibleVariables=new ObservableCollection<Variable>();
    }
    
    private void ExecuteSettingFactCommand(object? parameter)
    {
        var selectedVariables = PossibleVariables.Where(
                c=>!string.IsNullOrWhiteSpace(c.InputValue)
                   && c.Condition is not null
                   ).ToArray();
        OnFactCreated?.Invoke(selectedVariables);
        ResetVariablesValuesAndConditions(selectedVariables);
    }

    private bool CanExecuteSettingFactCommand(object? parameter)
    {
        if (parameter is IEnumerable<Variable> variables)
                return variables.Any(variable => !string.IsNullOrWhiteSpace(variable.InputValue));
        return false;
    }

    private void ResetVariablesValuesAndConditions(IEnumerable<Variable> variables)
    {
        foreach (var variable in variables)
        {
            variable.InputValue = string.Empty;
            variable.Condition = Condition.None;
        }
    }
    public ICommand SettingFactCommand { get; }
}