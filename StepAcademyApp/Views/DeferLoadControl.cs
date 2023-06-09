using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Metadata;

namespace StepAcademyApp.Views;

public sealed class DeferLoadControl : Control
{
    private IControl _control;
        
    public static readonly StyledProperty<bool> LoadProperty = AvaloniaProperty.Register<DeferLoadControl, bool>(
        "Load");

    public bool Load
    {
        get => GetValue(LoadProperty);
        set => SetValue(LoadProperty, value);
    }
        
    [Content, TemplateContent]
    public object DeferredContent { get; set; }

    public IControl Control => _control;

    static DeferLoadControl()
    {
        LoadProperty.Changed.AddClassHandler<DeferLoadControl>((c, e) =>
        {
            if (e.NewValue is bool v)
                c.DoLoad(v);
        });
    }
        
    protected override Size MeasureOverride(Size availableSize) 
        => LayoutHelper.MeasureChild(_control, availableSize, default);

    protected override Size ArrangeOverride(Size finalSize) 
        => LayoutHelper.ArrangeChild(_control, finalSize, default);


    private void DoLoad(bool load)
    {
        if((_control != null) == load)
            return;

        if (load)
        {
            _control = TemplateContent.Load(DeferredContent).Control;
            ((ISetLogicalParent)_control).SetParent(this);
            VisualChildren.Add(_control);
            LogicalChildren.Add(_control);
        }
        else
        {
            ((ISetLogicalParent)_control).SetParent(null);
            LogicalChildren.Clear();
            VisualChildren.Remove(_control);
            _control = null;
        }
        InvalidateMeasure();
    }
}