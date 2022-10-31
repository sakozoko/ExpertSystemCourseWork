﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ExpertSystemUI.Model;

namespace ExpertSystemUI.Command;

public class GetResultCommand : ICommand
{
    private readonly Action _action;

    public GetResultCommand(Action action)
    {
        _action = action;
    }

    public bool CanExecute(object? parameter)
    {
        if (parameter is IEnumerable<Variable> variables) return variables.Any();
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