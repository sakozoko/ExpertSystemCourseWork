using System.Collections.Generic;
using ExpertSystemUIRuleCreator.Service;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class MainViewModel : ViewBase
{
    public MainViewModel(IEnumerable<string> possibleConditions, RulesManager savingService)
    {
        TabItemRulesViewModel = new TabItemRulesViewModel(savingService);
        TabItemAddRuleViewModel = new TabItemAddRuleViewModel(savingService, possibleConditions);
    }

    public ViewBase TabItemRulesViewModel { get; }
    public ViewBase TabItemAddRuleViewModel { get; }
}