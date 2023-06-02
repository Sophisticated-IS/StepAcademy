using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
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
                desktop.MainWindow = new LoginWindow();
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
                var loginVM = (LoginWindowVM)desktop.MainWindow.DataContext;

                if (!loginVM.IsAuthorized)
                {
                    Environment.Exit(0);
                }

                var mainWindow = new MainWindow();
                mainWindow.DataContext = null;
                //делаем окно главным, вместо окна авторизации
                desktop.MainWindow = mainWindow;
                mainWindow.Show();
            }
        }
    }
}