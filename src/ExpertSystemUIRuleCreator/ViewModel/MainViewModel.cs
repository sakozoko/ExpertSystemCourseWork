using ExpertSystemUIRuleCreator.Service;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class MainViewModel : ViewBase
{
    private readonly RulesManager _rulesManager;
    public MainViewModel(RulesManager rulesManager)
    {
        _rulesManager = rulesManager;
        RulesViewModel = new RulesViewModel(rulesManager);
        CreatingRuleViewModel = new CreatingRuleViewModel(rulesManager);
    }

    public ViewBase RulesViewModel { get; }
    public ViewBase CreatingRuleViewModel { get; }

    
    
}