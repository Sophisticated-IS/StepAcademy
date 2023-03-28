using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using StepAcademyApp.Views;
using StepAcademyApp.Views.Application;

namespace StepAcademyApp
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            
            // Styles.Add((IStyle)Resources["DataGridFluent"]!);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new LoginWindow
                {
                    DataContext = null
                };
                desktop.ShutdownMode = ShutdownMode.OnMainWindowClose;
                desktop.MainWindow.Closing += Closing;
            }

            base.OnFrameworkInitializationCompleted();
        }
        
        /// <summary>
        /// Закрытие окна авторизации и открытие основного окна приложения  
        /// </summary>
        private void Closing(object? sender, CancelEventArgs cancelEventArgs)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow.Closing -= Closing;
                //todo проверку делать isAuthorized
                // var interactableViewModel = (IInteractableViewModel<object?>)desktop.MainWindow.DataContext!;
                // if (interactableViewModel.InteractionResult is null) return;

                var mainWindow = new MainWindow();
                mainWindow.DataContext = null;
                //делаем окно главным, вместо окна авторизации
                desktop.MainWindow = mainWindow;
                mainWindow.Show();
            }
        }
    }
}