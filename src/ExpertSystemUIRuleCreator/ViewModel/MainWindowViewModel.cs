using ExpertSystemUIRuleCreator.Service;
using Infrastructure;
using WPFBase.ViewModel.Base;

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