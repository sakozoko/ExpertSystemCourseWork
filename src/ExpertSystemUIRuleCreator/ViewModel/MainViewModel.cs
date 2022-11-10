using System.Collections.Generic;
using System.Collections.ObjectModel;
using ExpertSystemUIRuleCreator.Model;
using ExpertSystemUIRuleCreator.Service;
using ExpertSystemUIRuleCreator.ViewModel.Base;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class MainViewModel : ViewBase
{
    public ViewBase TabItemRulesViewModel { get; }
    public ViewBase TabItemAddRuleViewModel { get; }
    


    public MainViewModel(IEnumerable<string> possibleConditions, RuleSavingService savingService) 
    {

        
        TabItemRulesViewModel = new TabItemRulesViewModel(savingService);
        TabItemAddRuleViewModel = new TabItemAddRuleViewModel(savingService, possibleConditions);
        
    }
}