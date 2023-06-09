using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using StepAcademyApp.DataBase;
using StepAcademyApp.Models;

namespace StepAcademyApp.ViewModels;

internal sealed class TeacherSalaryVM : ViewModelBase
{
    internal sealed class TeacherSalary
    {
        public uint Год { get; set; }
        public ushort Месяц { get; set; }
        public decimal Зарплата { get; set; }
    }

    public ObservableCollection<TeacherSalary> РасчетЗарплаты { get; } = new();
    public TeacherSalaryVM()
    {
        if (Program.CurrentUser is not Преподаватель) return;

        ReadSalary();
    }

    private void ReadSalary()
    {
        try
        {
            using var dbContext = new StepAcademyDB(Program.DbContextOptions);
            var salary = dbContext.Зарплаты.Where(x => x.IdУчитель == Program.CurrentUser.Id).ToList();
            РасчетЗарплаты.Clear();
            foreach (var sal in salary)
            {
                РасчетЗарплаты.Add(new TeacherSalary
                {
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