using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StepAcademyApp.Views.Application;

public partial class TeacherScheduleUC : UserControl
{
    public TeacherScheduleUC()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}