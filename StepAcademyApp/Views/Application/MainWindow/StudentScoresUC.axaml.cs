using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StepAcademyApp.Views.Application;

public partial class StudentScoresUC : UserControl
{
    public StudentScoresUC()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}