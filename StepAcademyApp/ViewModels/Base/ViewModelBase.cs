using System;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;

namespace StepAcademyApp.ViewModels;

public abstract class ViewModelBase : ReactiveObject, IDisposable
{
    protected readonly CompositeDisposable CompositeDisposable = new ();

    public Interaction<Unit, Unit> CloseWindowInteraction { get; } = new();

    public void Dispose()
    {
        CompositeDisposable?.Dispose();
    }
}