using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ExpertSystem;
using ExpertSystemUI.Command;
using ExpertSystemUI.Model;
using ExpertSystemUI.ViewModel.Base;

namespace ExpertSystemUI.ViewModel;

public class MainWindowViewModel : ViewBase
{
    private readonly RuleInferenceEngineFacade _rief;
    private string? _result;
    private ObservableCollection<Variable> _userFacts;

    public MainWindowViewModel()
    {
        _userFacts = new ObservableCollection<Variable>();
        SetRulesCommand = new SetRulesCommand(SetFacts);
        GetResultCommand = new GetResultCommand(GetResult);
        _rief = new RuleInferenceEngineFacade();
        _rief.SetKnowledgeBase("knowledgeBase.json");
        PossibleVariables = new ObservableCollection<Variable>(_rief.Variables.Select(x => new Variable { Name = x }));
        PossibleConditions = _rief.PossibleConditions;
    }

    public ICommand SetRulesCommand { get; }
    public ICommand GetResultCommand { get; }

    public ObservableCollection<Variable> UserFacts
    {
        get => _userFacts;
        set => SetField(ref _userFacts, value);
    }

    public string? Result
    {
        get => _result;
        private set => SetField(ref _result, value);
    }

    public ObservableCollection<Variable> PossibleVariables { get; }

    public string[] PossibleConditions { get; }

    private void SetFacts()
    {
        _rief.ClearFacts();
        UserFacts.Clear();
        _rief.SetFacts(PossibleVariables.Select(x => x.Deconstruct()));
        _rief.Facts.ForEach(v =>
        {
            if (v is null) return;
            var firstOrDefaultFact = UserFacts.FirstOrDefault(c => v.Variable.Equals(c.Name));
            if (firstOrDefaultFact is null)
            {
                UserFacts.Add(new Variable { Name = v.Variable, Condition = v.Condition, InputValue = v.Value });
            }
            else
            {
                firstOrDefaultFact.InputValue = v.Value;
                firstOrDefaultFact.Condition = v.Condition;
            }
        });
    }

    private void GetResult()
    {
        Result = _rief.GetResult("");
    }
}