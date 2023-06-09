using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using StepAcademyApp.DataBase;
using StepAcademyApp.Models;

namespace StepAcademyApp.Services;

internal sealed class SalaryCalculator
{
    public SalaryCalculator()
    {
        
    }


    public decimal Calculate(Преподаватель преподаватель,byte month)
    {
        using var dbcontext = new StepAcademyDB(Program.DbContextOptions);
        var нагрузкаМесяц = dbcontext.Нагрузка.Where(x => x.ВремяКонцаЗанятия.Month == month)
                                     .Include(x=>x.Предмет)
                                     .Include(x=>x.ТипЗанятия);

        var расчётЗП = 0m;
        foreach (var нагрузка in нагрузкаМесяц)
        {
            var оплата = dbcontext.ОплатаЗанятий.First(x => x.IdПредмет == нагрузка.Предмет.Id
                                                            && x.IdТипЗанятия == нагрузка.ТипЗанятия.Id);

            var поправкаНаСтаж = оплата.ЧасоваяОплата * ((decimal)преподаватель.Стаж.TotalDays/365m / 100m);
            расчётЗП += поправкаНаСтаж;
        }
        
        return расчётЗП;
    }

    public decimal Calculate(Преподаватель преподаватель)
    {
        return Calculate(преподаватель, (byte)DateTime.Now.Month);
    }
}