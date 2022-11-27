using ExpertSystemUIRuleCreator.Service;
using ExpertSystemUIRuleCreator.ViewModel.Base;
using Infrastructure;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class MainWindowViewModel : ViewBase
{
    public MainWindowViewModel()
    {

        var ruleSource = Source.RepositoryFactory.CreateRuleRepository();

        var savingService = new RulesManager(ruleSource);
        MainViewModel = new MainViewModel(savingService);
    }

    public ViewBase MainViewModel { get; }
}