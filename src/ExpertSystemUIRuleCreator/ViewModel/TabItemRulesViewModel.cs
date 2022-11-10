using System.Collections.ObjectModel;
using System.Windows.Input;
using ExpertSystemUIRuleCreator.Command;
using ExpertSystemUIRuleCreator.Model;
using ExpertSystemUIRuleCreator.Service;
using ExpertSystemUIRuleCreator.View.Control;
using ExpertSystemUIRuleCreator.ViewModel.Base;
using MaterialDesignThemes.Wpf;

namespace ExpertSystemUIRuleCreator.ViewModel;

public class TabItemRulesViewModel : ViewBase
{
    private readonly RuleSavingService _savingService;

    public TabItemRulesViewModel(RuleSavingService ruleSavingService)
    {
        _savingService = ruleSavingService;
        Rules = ruleSavingService.Rules;
        RemoveRuleCommand = new LambdaCommand(ExecuteRemovingRule);
    }

    public ObservableCollection<RuleModel> Rules { get; }
    public ICommand RemoveRuleCommand { get; }

    private async void ExecuteRemovingRule(object? parameter)
    {
        if (parameter is not RuleModel rule) return;

        var view = new RemoveRuleDialog
        {
            DataContext = rule
        };
        var res = await DialogHost.Show(view, "RootDialog");
        if (res is true) _savingService.Remove(rule);
    }
}