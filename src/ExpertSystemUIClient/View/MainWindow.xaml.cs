using System.Windows;
using ExpertSystemClientService;
using ExpertSystemUI.ViewModel;

namespace ExpertSystemUI.View;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Resources["PossibleConditions"] = new[] { "=", ">", "<" };
        var ruleRepos = Infrastructure.Source.RepositoryFactory.CreateRuleRepository();
        var clauseRepos = Infrastructure.Source.RepositoryFactory.CreateClauseRepository();
        var rief = new RuleInferenceEngineFacade(ruleRepos, clauseRepos);
        DataContext = new MainWindowViewModel(rief);
    }
}