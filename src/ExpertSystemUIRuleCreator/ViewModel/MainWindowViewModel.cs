using ExpertSystemUIRuleCreator.Service;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class MainWindowViewModel : ViewBase
{
    public MainWindowViewModel()
    {
        var possibleConditions = new[] { "=", "<", ">" };

        var ruleSource = new JsonRuleSource("knowledgeBase.json");

        var savingService = new RuleSavingService(ruleSource);
        MainViewModel = new MainViewModel(possibleConditions, savingService);
    }

    public ViewBase MainViewModel { get; }
}