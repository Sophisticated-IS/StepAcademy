using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SharpHash.Base;
using StepAcademyApp.DataBase;
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
            var dialogService = new UserDialogService();
            await dialogService.ShowMessageForbidden("Ошибка авторизации","Неверный логин или пароль").ConfigureAwait(false);
        }

    }

    private bool CheckUserCredentials()
    {
        using var dbContext = new StepAcademyDB(Program.DbContextOptions);

        

        var user = dbContext.УчетныеДанные.Where(x => x.Логин == Login).Include(x => x.Гражданин).FirstOrDefault();

        string ps = HashFactory.Crypto.CreateGOST3411_2012_512().ComputeString(Login + user.Соль, Encoding.UTF8).ToString();

        if (ps == user.Пароль)
        {
            if (user is null)
            {
                return false;
            }
            else
            {
                Program.CurrentUser = user.Гражданин;
                return true;
            }
        }
        else
            return false;
    } 
}