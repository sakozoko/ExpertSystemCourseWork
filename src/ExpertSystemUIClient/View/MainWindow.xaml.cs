﻿using System.Windows;

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
    }
}