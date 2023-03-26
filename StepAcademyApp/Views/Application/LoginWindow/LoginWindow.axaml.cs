using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace StepAcademyApp.Views.Application;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}