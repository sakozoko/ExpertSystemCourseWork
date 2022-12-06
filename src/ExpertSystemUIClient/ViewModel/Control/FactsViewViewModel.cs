using System.Collections.ObjectModel;
using System.Windows.Input;
using ExpertSystemUI.Model;
using WPFBase.Command;
using WPFBase.ViewModel.Base;

namespace ExpertSystemUI.ViewModel.Control;

public class FactsViewViewModel : ViewBase
{
    public ObservableCollection<Variable> Facts { get; set;}

    public FactsViewViewModel()
    {
        Facts = new ObservableCollection<Variable>();
        RemovingFactCommand = new LambdaCommand(ExecuteRemovingFactCommand);
        
    }
    private void ExecuteRemovingFactCommand(object? parameter)
    {
        if (parameter is Variable variable)
        {
            Facts.Remove(variable);
        }
    }
    public ICommand RemovingFactCommand { get; }
    
}