using ExpertSystemUIRuleCreator.Service;
using ExpertSystemUIRuleCreator.ViewModel.Base;
using Infrastructure.Repository;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class MainWindowViewModel : ViewBase
{
    public MainWindowViewModel()
    {
        var possibleConditions = new[] { "=", "<", ">" };

        var ruleSource = new RuleRepositoryJson("knowledgeBase.json");

        var savingService = new RulesManager(ruleSource);
        MainViewModel = new MainViewModel(possibleConditions, savingService);
    }

    public ViewBase MainViewModel { get; }
}