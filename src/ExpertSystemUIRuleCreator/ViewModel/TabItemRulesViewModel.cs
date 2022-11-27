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
    private readonly RulesManager _rulesManager;

    public TabItemRulesViewModel(RulesManager rulesManager)
    {
        _rulesManager = rulesManager;
        Rules = rulesManager.Rules;
        RemoveRuleCommand = new LambdaCommand(ExecuteRemovingRule);
        EditRuleCommand = new LambdaCommand(ExecuteEditingRule);
    }

    public ObservableCollection<RuleModel> Rules { get; }
    public ICommand RemoveRuleCommand { get; }
    public ICommand EditRuleCommand { get; }

    private async void ExecuteRemovingRule(object? parameter)
    {
        if (parameter is not RuleModel rule) return;

        var view = new RemoveRuleDialog
        {
            DataContext = rule
        };
        var res = await DialogHost.Show(view, "RootDialog");
        if (res is true) _rulesManager.Remove(rule);
    }

    private async void ExecuteEditingRule(object? parameter)
    {
        if (parameter is not RuleModel rule) return;
        var editingRule = (RuleModel)rule.Clone();
        var viewModel = new ModificationRuleViewModel(_rulesManager)
        {
            Rule = editingRule
        };
        var view = new EditingRuleDialog()
        {
            DataContext = viewModel
        };
        var result = await DialogHost.Show(view, "RootDialog");
        if (result is true) _rulesManager.Update(editingRule);
    }
}