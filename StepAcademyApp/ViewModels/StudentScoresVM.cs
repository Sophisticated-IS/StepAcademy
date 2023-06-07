﻿using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StepAcademyApp.DataBase;


namespace StepAcademyApp.ViewModels;

internal sealed class StudentScoresVM
{
    public ObservableCollection<StudentScore> Оценки { get; } = new();
    
    internal sealed class StudentScore
    {
        public string Специальность { get; set; }
        public string Отделение { get; set; }
        public string НазваниеПредмета { get; set; }
        public string Оценка { get; set; }
    } 
    public StudentScoresVM()
    {
       ReadScores();
    }

    public void ReadScores()
    {
        var dbContext = new StepAcademyDB(Program.DbContextOptions);
        var scores = dbContext.Оценки.Where(x => x.IdСтудента == Program.CurrentUser.Id)
                              .Include(x=>x.Группа).Include(x=>x.Предмет).ToList();
        Оценки.Clear();
        foreach (var score in scores)
        {
            Оценки.Add(new StudentScore
            {
                НазваниеПредмета = score.Предмет.Name,
                Специальность = score.Группа.Специальность.Name,
                Отделение = score.Группа.Отделение.Name,
                Оценка = score.Балл.ToString()
            });
        }
    } 
}