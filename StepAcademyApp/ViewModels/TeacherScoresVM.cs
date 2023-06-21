using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StepAcademyApp.DataBase;
using StepAcademyApp.Models;

namespace StepAcademyApp.ViewModels;

internal sealed class TeacherScoresVM : ViewModelBase
{
    public ObservableCollection<MockClass> Оценки { get; } = new();

    internal sealed class MockClass
    {
        public string ФИОСтудента { get; set; }
        public string Специальность { get; set; }
        public string Отделение { get; set; }
        public string НазваниеПредмета { get; set; }
        public string Оценка { get; set; }
    } 
    public TeacherScoresVM()
    {
        if (Program.CurrentUser is not Преподаватель) return;

        using var dbContext = new StepAcademyDB(Program.DbContextOptions);

        var нагрузки = dbContext.Нагрузка.Where(
            x => x.IdПреподавателя == Program.CurrentUser.Id
        ).ToList();

        List<Models.ГруппаСтудентов> groupSt = new List<ГруппаСтудентов>();

        foreach(var item in нагрузки)
        {
            Models.ГруппаСтудентов group = dbContext.ГруппыСтудентов.Where(
                    x => x.Id == item.IdГруппы
                ).FirstOrDefault();
            if (!groupSt.Any(item => item.Id == group.Id))
            {
                groupSt.Add(group);
            }
        }

        List<Models.Студент> students = new List<Студент>();

        foreach(var item in groupSt)
        {
            students.AddRange(dbContext.Студенты.Where(
                    x => x.IdГруппы == item.Id
                ).ToList()
            );
            
        }

        foreach(var item in students)
        {
            var ocen = dbContext.Оценки.Where(
                x => x.IdСтудента == item.Id
            ).FirstOrDefault();
            var fio = "";
            fio = item.Фамилия + " " + item.Имя + " " + item.Отчество;
            var group = dbContext.ГруппыСтудентов.Where(
                x => x.Id == item.IdГруппы
            ).FirstOrDefault();
            string spec = dbContext.Специальности.Where(
                x => x.Id == group.СпециальностьId
            ).FirstOrDefault().Название;
            string otdel = dbContext.Отделения.Where(
                x => x.Id == group.ОтделениеId
            ).FirstOrDefault().Название;
            string predmet = dbContext.Предметы.Where(
                x => x.Id == ocen.IdПредмета
            ).FirstOrDefault().Название;
            Оценки.Add(
                new MockClass
                {
                    ФИОСтудента = fio,
                    Специальность = spec,
                    Отделение = otdel,
                    НазваниеПредмета = predmet,
                    Оценка = ocen.Балл.ToString()
                }
            );
        }
    }
}