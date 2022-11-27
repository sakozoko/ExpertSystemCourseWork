
using ExpertSystemUIRuleCreator.Service;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class MainViewModel : ViewBase
{
    public MainViewModel(RulesManager savingService)
    {
        TabItemRulesViewModel = new TabItemRulesViewModel(savingService);
        ModificationRuleViewModel = new ModificationRuleViewModel(savingService);
    }

    public ViewBase TabItemRulesViewModel { get; }
    public ViewBase ModificationRuleViewModel { get; }
}