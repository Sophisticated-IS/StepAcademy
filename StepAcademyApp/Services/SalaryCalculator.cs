using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StepAcademyApp.DataBase;
using StepAcademyApp.Models;

namespace StepAcademyApp.Services;

internal sealed class SalaryCalculator
{
    public SalaryCalculator()
    {
        
    }


    public async Task<decimal> Calculate(Преподаватель преподаватель,byte month)
    {
        await using var dbcontext = new StepAcademyDB(Program.DbContextOptions);
        var нагрузкаМесяц = await dbcontext.Нагрузка.Where(x => x.IdПреподавателя == преподаватель.Id && x.ВремяКонцаЗанятия.Year == DateTime.Now.Year && x.ВремяКонцаЗанятия.Month == month)
                                     .Include(x=>x.Предмет)
                                     .Include(x=>x.ТипЗанятия).ToListAsync();

        var расчётЗП = 0m;
        foreach (var нагрузка in нагрузкаМесяц)
        {
            var оплата = await dbcontext.ОплатаЗанятий.FirstAsync(x => x.IdПредмет == нагрузка.Предмет.Id
                                                            && x.IdТипЗанятия == нагрузка.ТипЗанятия.Id);

            var поправкаНаСтаж = оплата.ЧасоваяОплата * ((decimal)преподаватель.Стаж.TotalDays/365m / 100m);
            расчётЗП += поправкаНаСтаж;
        }
        
        return расчётЗП;
    }

    public Task<decimal> Calculate(Преподаватель преподаватель)
    {
        return Calculate(преподаватель, (byte)DateTime.Now.Month);
    }
}