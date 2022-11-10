using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ExpertSystemUIRuleCreator.Command;
using ExpertSystemUIRuleCreator.Extension;
using ExpertSystemUIRuleCreator.Model;
using ExpertSystemUIRuleCreator.Service;
using ExpertSystemUIRuleCreator.View;
using ExpertSystemUIRuleCreator.ViewModel.Base;
using MaterialDesignThemes.Wpf;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class MainWindowViewModel : ViewBase
{
    public ViewBase MainViewModel { get; }

    public MainWindowViewModel()
    {
        var possibleConditions = new[] { "=", "<", ">" };

        var ruleSource = new JsonRuleSource("knowledgeBase.json");

        var savingService = new RuleSavingService(ruleSource);
        MainViewModel = new MainViewModel(possibleConditions, savingService);
    }
    
}