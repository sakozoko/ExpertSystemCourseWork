using System.Windows;
using ExpertSystemClientService;
using ExpertSystemUI.ViewModel;
using Infrastructure.Repository;

namespace ExpertSystemUI.View;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        const string dataFilePath="knowledgeBase.json";
        Resources["PossibleConditions"] = new[] { "=", ">", "<" };
        var ruleRepos = new RuleRepositoryJson(dataFilePath);
        var clauseRepos = new ClauseRepositoryJson(ruleRepos.GetAll().Result);
        var rief = new RuleInferenceEngineFacade(ruleRepos, clauseRepos);
        DataContext = new MainWindowViewModel(rief);
    }
}