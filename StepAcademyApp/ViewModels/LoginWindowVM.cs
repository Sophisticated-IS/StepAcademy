using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using StepAcademyApp.Services;
using ViewModelBase = StepAcademyApp.ViewModels.ViewModelBase;

namespace StepAcademyApp;

public class LoginWindowVM : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> AuthUserCommand { get; }
    public bool IsAuthorized { get; set; }

    [Reactive]
    public string Login { get; set; }

    [Reactive]
    public string Password { get; set; }

    public LoginWindowVM()
    {
        var canSignIn = this.WhenAnyValue(v => v.Login, v => v.Password, (x, y) =>
            !string.IsNullOrEmpty(x) && !string.IsNullOrEmpty(y));
        AuthUserCommand = ReactiveCommand.CreateFromTask(AuthUser, canSignIn);
        Login = "test";
        Password = "test";
    }

    private async Task AuthUser()
    {
        if (CheckUserCredentials())
        {
            IsAuthorized = true;
            await CloseWindowInteraction.Handle(Unit.Default);
        }
        else
        {
            var ud = new UserDialogService();
            await ud.ShowMessageForbidden("Ошибка авторизации","Неверный логин или пароль").ConfigureAwait(false);
        }

    }

    private bool CheckUserCredentials()
    {
        return Login=="test" && Password=="test";
    } 
}