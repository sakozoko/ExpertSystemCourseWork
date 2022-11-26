
using ExpertSystemUIRuleCreator.Service;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class MainViewModel : ViewBase
{
    public MainViewModel(RulesManager savingService)
    {
        TabItemRulesViewModel = new TabItemRulesViewModel(savingService);
        TabItemAddRuleViewModel = new TabItemAddRuleViewModel(savingService);
    }

    public ViewBase TabItemRulesViewModel { get; }
    public ViewBase TabItemAddRuleViewModel { get; }
}