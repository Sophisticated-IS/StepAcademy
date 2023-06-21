using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
using StepAcademyApp.Models.LocalizedAttributes;
using StepAcademyApp.Services;
using ViewModelBase = StepAcademyApp.ViewModels.ViewModelBase;

namespace StepAcademyApp;

public class LoginWindowVM : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> AuthUserCommand { get; }
    public bool IsAuthorized { get; set; }

    public bool IsBanned {get; set; } = false;

    [Reactive]
    [MinLengthAttributeCustom(1)]
    public string Login { get; set; } 

    [Reactive]
    [RegularExpressionPassword("^(?=.*[A-Z])(?=.*[0-9])[A-Za-z0-9]+$")]
    [MinLengthAttributeCustom(10)]
    public string Password { get; set; }

    public LoginWindowVM()
    {
        var canSignIn = this.WhenAnyValue(v => v.Login, v => v.Password, (x, y) =>
            !string.IsNullOrEmpty(x) && !string.IsNullOrEmpty(y) 
                                     && Validator.TryValidateObject(this, new ValidationContext(this),
                new List<ValidationResult>()));
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
            if (IsBanned) {
                await dialogService.ShowMessageForbidden("Ошибка авторизации","Пользователь заблокирован. Обратитесь к системному администратору.").ConfigureAwait(false);
            }
            else
            {
                await dialogService.ShowMessageForbidden("Ошибка авторизации","Неверный логин или пароль").ConfigureAwait(false);
            }
        }

    }

    private bool CheckUserCredentials()
    {
        using var dbContext = new StepAcademyDB(Program.DbContextOptions);

        var user = dbContext.УчетныеДанные.Where(x => x.Логин == Login).Include(x => x.Гражданин).FirstOrDefault();

        if (user is null) return false;
        
        string ps = HashFactory.Crypto.CreateGOST3411_2012_512().ComputeString(Password + user.Соль, Encoding.UTF8).ToString();

        if (user is null)
        {
            Models.Аудит аудит;
            аудит = new Models.Аудит{
                Message = $"Error of login by {Login}",
                Time = DateTime.SpecifyKind(System.DateTime.Now, DateTimeKind.Utc)
            };
            dbContext.Аудит.Add(аудит);
            dbContext.SaveChanges();
            return false;
        }
        else
        {
            if (user.isBanned != true)
            {
                if (ps == user.Пароль)
                {
                    Models.Аудит аудит;
                    аудит = new Models.Аудит{
                        Message = $"Login by {Login}",
                        Time = DateTime.SpecifyKind(System.DateTime.Now, DateTimeKind.Utc)
                    };
                    user.NumberOfBadLogging = 0;
                    dbContext.УчетныеДанные.Update(user);
                    dbContext.Аудит.Add(аудит);
                    dbContext.SaveChanges();
                    Program.CurrentUser = user.Гражданин;
                    return true;
                }
                else
                {
                    Models.Аудит аудит;
                    аудит = new Models.Аудит{
                            Message = $"Error of login by {Login}",
                            Time = DateTime.SpecifyKind(System.DateTime.Now, DateTimeKind.Utc)
                        };
                    dbContext.Аудит.Add(аудит);
                    user.NumberOfBadLogging++;
                    if (user.NumberOfBadLogging >= 5)
                    {
                        IsBanned = true;
                        user.isBanned = true;
                        аудит = new Models.Аудит{
                            Message = $"Ban user by login - {Login}",
                            Time = DateTime.SpecifyKind(System.DateTime.Now, DateTimeKind.Utc)
                        };
                    dbContext.Аудит.Add(аудит);
                    }
                    dbContext.УчетныеДанные.Update(user);
                    dbContext.SaveChanges();
                    return false;
                }
            }
            else
            {
                IsBanned = true;
                return false;
            }
        }
    } 
}