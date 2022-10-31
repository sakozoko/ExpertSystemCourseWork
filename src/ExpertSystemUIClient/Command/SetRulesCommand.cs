using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ExpertSystemUI.Model;

namespace ExpertSystemUI.Command;

public class SetRulesCommand : ICommand
{
    private readonly Action _action;

    public SetRulesCommand(Action action)
    {
        _action = action;
    }

    public bool CanExecute(object? parameter)
    {
        if (parameter is IEnumerable<Variable> variables)
            return variables.Any(variable => !string.IsNullOrWhiteSpace(variable.InputValue));
        return false;
    }

    public void Execute(object? parameter)
    {
        _action.Invoke();
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;

        remove => CommandManager.RequerySuggested -= value;
    }
}