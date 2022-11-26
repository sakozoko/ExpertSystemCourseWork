using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ExpertSystem;
using ExpertSystemUI.Model;
using ExpertSystemUI.ViewModel.Base;
using ExpertSystemUI.ViewModel.Control;
using Infrastructure.Repository;

namespace ExpertSystemUI.ViewModel;

public class MainWindowViewModel : ViewBase
{
    private RuleInferenceEngineFacade _rief;
    private const string FilePath = "knowledgeBase.json";

    public CreatingFactViewViewModel CreatingFactViewViewModel { get;}
    public FactsViewViewModel FactsViewViewModel { get; }
    public ResultViewViewModel ResultViewViewModel { get; }

    public MainWindowViewModel()
    {
        var ruleRepos = new RuleRepositoryJson(FilePath);
        var clauseRepos = new ClauseRepositoryJson(ruleRepos.GetAll().Result);
        _rief = new RuleInferenceEngineFacade(ruleRepos, clauseRepos);
        var possibleVariables = new ObservableCollection<Variable>(_rief.GetClauseNames().Result.Select(x => new Variable { Name = x ?? string.Empty }));

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