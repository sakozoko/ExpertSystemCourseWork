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
    private readonly RulesManager _savingService;

    public TabItemRulesViewModel(RulesManager rulesManager)
    {
        _savingService = rulesManager;
        Rules = rulesManager.Rules;
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