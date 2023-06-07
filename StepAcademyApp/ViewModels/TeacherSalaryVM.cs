using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using StepAcademyApp.DataBase;

namespace StepAcademyApp.ViewModels;

internal sealed class TeacherSalaryVM
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