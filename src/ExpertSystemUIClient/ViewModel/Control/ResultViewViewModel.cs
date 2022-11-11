using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ExpertSystemUI.Command;
using ExpertSystemUI.Model;
using ExpertSystemUI.ViewModel.Base;

namespace ExpertSystemUI.ViewModel.Control;

public class ResultViewViewModel : ViewBase
{
    private string _result;
    public IEnumerable<Variable>? Facts { get; set; }
    
    public Func<IEnumerable<Variable>, string>? GetResultValue { get; set; }

    public string Result
    {
        get => _result;
        set => SetField(ref _result, value);
    }
    
    
    public ResultViewViewModel()
    {
        _result= "ResultTest";
        ResolvingCommand = new LambdaCommand(ExecuteResolvingCommand);
        CleaningCommand = new LambdaCommand(ExecuteCleaningCommand, CanExecuteCleaningCommand);
    }
    

    private void ExecuteResolvingCommand(object? parameter)
    {
        if (Facts?.Any() ?? false)
        {
            Result = GetResultValue?.Invoke(Facts)?? "GetResultValue is not setted";
            return;
        }
        Result="Facts is not setted";
    }
    
    private bool CanExecuteCleaningCommand(object? parameter) => !string.IsNullOrWhiteSpace(Result);
    private void ExecuteCleaningCommand(object? parameter)=>Result = string.Empty;
    
    public ICommand ResolvingCommand { get; }
    public ICommand CleaningCommand { get; }
}