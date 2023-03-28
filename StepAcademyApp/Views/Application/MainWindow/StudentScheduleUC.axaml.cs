using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StepAcademyApp.Views.Application;

public partial class StudentScheduleUC : UserControl
{
    public StudentScheduleUC()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}