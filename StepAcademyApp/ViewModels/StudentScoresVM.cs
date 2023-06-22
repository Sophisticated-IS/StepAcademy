using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StepAcademyApp.DataBase;
using StepAcademyApp.Models;


namespace StepAcademyApp.ViewModels;

internal sealed class StudentScoresVM : ViewModelBase
{
    public ObservableCollection<StudentScore> Оценки { get; } = new();
    
    internal sealed class StudentScore
    {
        public string Специальность { get; set; }
        public string Отделение { get; set; }
        public string НазваниеПредмета { get; set; }
        public string ФИОУчителя { get; set; }
        public string Оценка { get; set; }
    } 
    public StudentScoresVM()
    {
        if (Program.CurrentUser is not Студент) return;

       ReadScores();
    }

    private void ReadScores()
    {
        try
        {
            var dbContext = new StepAcademyDB(Program.DbContextOptions);
            var scores = dbContext.Оценки.Where(x => x.IdСтудента == Program.CurrentUser.Id)
                                  .Include(x=>x.Группа)
                                  .Include(x=>x.Группа.Специальность)
                                  .Include(x=>x.Группа.Отделение)
                                  .Include(x=>x.Предмет)
                                  .ToList();
            Оценки.Clear();
            foreach (var score in scores)
            {
                var nagruz = dbContext.Нагрузка.Where(
                    x => x.IdГруппы == score.Группа.Id
                ).FirstOrDefault();
                var teacher = dbContext.Учителя.Where(
                    x => x.Id == nagruz.IdПреподавателя
                ).FirstOrDefault();
                Оценки.Add(new StudentScore
                {
                    НазваниеПредмета = score.Предмет.Название,
                    Специальность = score.Группа.Специальность.Название,
                    Отделение = score.Группа.Отделение.Название,
                    Оценка = score.Балл.ToString(),
                    ФИОУчителя = teacher.Фамилия + " " + teacher.Имя + " "+ teacher.Отчество
                });
            }
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
        }
    } 
}