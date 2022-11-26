using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ExpertSystemClientService;
using ExpertSystemUI.Model;
using ExpertSystemUI.ViewModel.Base;
using ExpertSystemUI.ViewModel.Control;

namespace ExpertSystemUI.ViewModel;

public class MainWindowViewModel : ViewBase
{
    private RuleInferenceEngineFacade _rief;
    public CreatingFactViewViewModel CreatingFactViewViewModel { get;}
    public FactsViewViewModel FactsViewViewModel { get; }
    public ResultViewViewModel ResultViewViewModel { get; }

    public MainWindowViewModel(RuleInferenceEngineFacade rief)
    {
        _rief = rief;
        var possibleVariables = new ObservableCollection<Variable>(_rief.GetClauseNames().Result.Select(x => new Variable { Name = x }));

        FactsViewViewModel = new FactsViewViewModel();

        ResultViewViewModel= new ResultViewViewModel
        {
            Facts = FactsViewViewModel.Facts,
            GetResultValue = ExecuteResolvingCommand
        };

        CreatingFactViewViewModel = new CreatingFactViewViewModel
        {
            PossibleVariables = possibleVariables,
            OnFactCreated = CreatingFactViewModelOnFactCreated
        };
    }

    private string ExecuteResolvingCommand(IEnumerable<Variable> facts)
    {
        _rief.SetFacts(FactsViewViewModel.Facts.Select(x => x.Deconstruct()));
        var result= _rief.GetResult("");
        return result;
    }

    private void CreatingFactViewModelOnFactCreated(IEnumerable<Variable> obj)
    {
        //add only unique variables to user facts, if value is not unique, update it
        foreach (var variable in obj)
        {
            var firstOrDefaultFact = FactsViewViewModel.Facts.FirstOrDefault(c => variable.Name.Equals(c.Name));
            if (firstOrDefaultFact is null)
            {
                FactsViewViewModel.Facts.Add((Variable)variable.Clone());
            }
            else
            {
                firstOrDefaultFact.InputValue = variable.InputValue;
                firstOrDefaultFact.Condition = variable.Condition;
            }
        }
    }

}