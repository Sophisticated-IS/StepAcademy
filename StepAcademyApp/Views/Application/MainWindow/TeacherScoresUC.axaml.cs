using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StepAcademyApp.Views.Application;

public partial class TeacherScoresUC : UserControl
{
    public TeacherScoresUC()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}