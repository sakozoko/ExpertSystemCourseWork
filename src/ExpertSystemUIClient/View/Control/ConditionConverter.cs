using System;
using System.Windows.Data;
using ExpertSystemUI.Model;

namespace ExpertSystemUI.View.Control;

public class ConditionConverter : IValueConverter
{
    // Convert from Condition enum value to int
    public object Convert(object? value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return (int)(value??-1);
    }
    // Convert from  int to Condition enum value
    public object ConvertBack(object? value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return (Condition)(value??0);
    }
}