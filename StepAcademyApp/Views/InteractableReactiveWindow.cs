using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ReactiveUI;
using ViewModelBase = StepAcademyApp.ViewModels.ViewModelBase;

namespace StepAcademyApp.Views;

/// <summary>
/// Окно реализующее Interactions во ViewModel 
/// </summary>
public abstract class InteractableReactiveWindow<TViewModel> : ReactiveWindow<TViewModel> where TViewModel : ViewModelBase
{
    
    protected InteractableReactiveWindow()
    {
        this.WhenAnyValue(d => d.DataContext)
            .Where(d => d is TViewModel).Subscribe(_ => DataContextInitialized());
    }

    /// <summary>
    /// Инициализация ReactiveUI.Interactions
    /// </summary>
    protected abstract void InteractionsInitialization(); 
    
    /// <summary>
    /// Событие инициализации DataContext окна нужным типом и не null значением
    /// </summary>
    private void DataContextInitialized()
    {
        InteractionsInitialization();
        ViewModel!.CloseWindowInteraction.RegisterHandler(CloseWindow);
    }

  
    /// <summary>
    /// Закрытие главного окна должно быть в основном UI Thread
    /// </summary>
    private async Task CloseWindow(InteractionContext<Unit, Unit> interaction)
    {
        await Dispatcher.UIThread.InvokeAsync(Close).ConfigureAwait(false);
        interaction.SetOutput(Unit.Default);
    }
    
}