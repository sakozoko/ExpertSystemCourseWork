using System.Collections.Generic;
using System.Windows.Controls;

namespace ExpertSystemUI.View.Control;

public partial class VariableViewControl : UserControl
{
    public VariableViewControl()
    {
        InitializeComponent();
        PossibleConditions = new List<string>();
        Resources["ConditionConverter"] = new ConditionConverter();
    }
    
    public IEnumerable<string> PossibleConditions { get; set; }

}