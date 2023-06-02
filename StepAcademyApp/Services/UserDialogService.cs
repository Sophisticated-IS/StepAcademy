using System.Threading.Tasks;
using Avalonia.Threading;
using MessageBox.Avalonia.Enums;
namespace StepAcademyApp.Services;
public sealed class UserDialogService
{
 
    public UserDialogService()
    {
        
    }

    public Task ShowMessageInfo(string title, string message)
    {
        return
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow(title, message, ButtonEnum.Ok, Icon.Info)
                    .Show();
            });
    }

    public Task ShowMessageWarning(string title, string message)
    {
        return
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow(title, message, ButtonEnum.Ok, Icon.Warning)
                    .Show();
            });
    }

    public Task ShowMessageError(string title, string message)
    {
        return
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow(title, message, ButtonEnum.Ok, Icon.Error)
                    .Show();
            });
    }

    public Task ShowMessageForbidden(string title, string message)
    {
        return
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                MessageBox.Avalonia.MessageBoxManager
                    .GetMessageBoxStandardWindow(title, message, ButtonEnum.Ok, Icon.Forbidden)
                    .Show();
            });
    }
    
    /// <summary>
    /// Окно подтверждения операции
    /// </summary>
    /// <returns>true - операция подтверждена false - операция отменена </returns>
    public async Task<bool> ShowAckOperationWindow(string title, string message)
    {
        var res = await Dispatcher.UIThread.InvokeAsync(async () =>
        {
            var dlg = await MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                                title, message, ButtonEnum.OkCancel, Icon.Question)
                            .Show().ConfigureAwait(false);
            return dlg == ButtonResult.Ok;
        });

        return res;
    }
}