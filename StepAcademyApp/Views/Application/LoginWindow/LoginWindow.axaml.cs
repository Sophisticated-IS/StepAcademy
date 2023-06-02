using Avalonia;
using Avalonia.Markup.Xaml;

namespace StepAcademyApp.Views.Application;

public partial class LoginWindow : InteractableReactiveWindow<LoginWindowVM>
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

    protected override void InteractionsInitialization()
    {
        
    }
}