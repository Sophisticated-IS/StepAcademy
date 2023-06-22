using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using StepAcademyApp.DataBase;
using StepAcademyApp.Models;
using StepAcademyApp.Services;

namespace StepAcademyApp.ViewModels;

internal sealed class TeacherSalaryVM : ViewModelBase
{
    internal sealed class TeacherSalary
    {
        public uint Id { get; init; }
        public string ФИО { get; set; }
        public uint Год { get; set; }
        public ushort Месяц { get; set; }
        public decimal Зарплата { get; set; }
    }

    public ReactiveCommand<Unit,Unit> CalcSalaryCommand { get; set; }
    public ReactiveCommand<Unit,Unit> SaveSalaryCommand { get; set; }
    public bool IsAdmin { get; set; }
    [Range(1,12)]
    public byte SalaryCalcMonth { get; set; }
    public ObservableCollection<TeacherSalary> РасчетЗарплаты { get; } = new();
    public TeacherSalaryVM()
    {
        if (Program.CurrentUser is not Преподаватель преподаватель) return;
        IsAdmin = преподаватель.Админ;

        Func<Зарплата, int, bool> func;
        LoadSalary();
    }

    private void LoadSalary()
    {
        РасчетЗарплаты.Clear();
        Func<Зарплата, int, bool> func;
        if (IsAdmin)
        {
            SalaryCalcMonth = (byte)DateTime.Now.Month;
            SaveSalaryCommand = ReactiveCommand.Create(SaveSalary);
            CalcSalaryCommand = ReactiveCommand.CreateFromTask(CalcSalary);
            func = (x, i) => true;
        }
        else
        {
            func = (x, i) => x.IdУчитель == Program.CurrentUser.Id;
        }

        ReadSalary(func);
    }

    private async Task CalcSalary()
    {
        LoadSalary();
        if (SalaryCalcMonth is <= 0 or >= 13) return;
        var salaryCalc = new SalaryCalculator();
        await using var dbContext = new StepAcademyDB(Program.DbContextOptions);
        var teachers = await dbContext.Учителя.ToListAsync();
        foreach (var teacher in teachers)
        {
            var salary = await salaryCalc.Calculate(teacher,SalaryCalcMonth);
            РасчетЗарплаты.Add(new TeacherSalary
            {
                Id = teacher.Id,
                ФИО = teacher.Имя + " " + teacher.Фамилия + " " + teacher.Отчество,
                Год = (uint)DateTime.Now.Year,
                Месяц = SalaryCalcMonth,
                Зарплата = salary
                
            });
        }
        var dialogService = new UserDialogService();
        await dialogService.ShowMessageInfo("Расчет", "Расчет проведен успешно").ConfigureAwait(false);
    }

    private void SaveSalary()
    {
        try
        {
            using var dbContext = new StepAcademyDB(Program.DbContextOptions);
            var salaryforMonth = РасчетЗарплаты.Where(x => x.Месяц == SalaryCalcMonth).ToList();
            foreach (var teacherSalary in salaryforMonth)
            {
                var firstOrDefault = dbContext.Зарплаты.FirstOrDefault(x=>x.IdУчитель == teacherSalary.Id 
                                                                          && x.Month == SalaryCalcMonth);

                Зарплата зп;
                if (firstOrDefault is null)
                {
                    зп = new Зарплата
                    {
                        IdУчитель = teacherSalary.Id,
                        Year = (uint)DateTime.Now.Year,
                        Month = SalaryCalcMonth,
                        СуммаВыплат = teacherSalary.Зарплата
                    };
                    dbContext.Зарплаты.Add(зп);
                }
                else
                {
                    firstOrDefault.СуммаВыплат = teacherSalary.Зарплата;
                    dbContext.Зарплаты.Update(firstOrDefault);
                }
                
                dbContext.SaveChanges();
            }
        }
        catch (Exception exception)
        {
            //ignored
        }
    }

    private void ReadSalary(Func<Зарплата, int, bool> func)
    {
        try
        {
            using var dbContext = new StepAcademyDB(Program.DbContextOptions);
            var salary = dbContext.Зарплаты.Include(x=>x.Преподаватель).Where(func).ToList();
            РасчетЗарплаты.Clear();
            foreach (var sal in salary)
            {
                РасчетЗарплаты.Add(new TeacherSalary
                {
                    Id = sal.Преподаватель.Id,
                    ФИО = sal.Преподаватель.Имя + " " + sal.Преподаватель.Фамилия + " " + sal.Преподаватель.Отчество,
                    Год = sal.Year,
                    Месяц = (ushort)sal.Month,
                    Зарплата = sal.СуммаВыплат
                });
            }
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
        }
    }
}