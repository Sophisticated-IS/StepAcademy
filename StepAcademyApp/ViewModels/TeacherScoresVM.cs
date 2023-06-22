using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using StepAcademyApp.DataBase;
using StepAcademyApp.Models;
using StepAcademyApp.Services;

namespace StepAcademyApp.ViewModels;

internal sealed class TeacherScoresVM : ViewModelBase
{
    public ObservableCollection<MockClass> Оценки { get; } = new();

    public ReactiveCommand<Unit, Unit> CancelChangesCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveChangesCommand { get; }
    internal sealed class MockClass
    {
        public string ФИОСтудента { get; set; }
        public string Специальность { get; set; }
        public string Отделение { get; set; }
        public string НазваниеПредмета { get; set; }
        public uint ИдОценки { get; set; }
        public short Оценка { get; set; }
    } 
    public TeacherScoresVM()
    {
        if (Program.CurrentUser is not Преподаватель) return;

        
        CancelChangesCommand = ReactiveCommand.Create(CancelChanges);
        SaveChangesCommand = ReactiveCommand.Create(SaveChanges);
        
        LoadTeacherScores();
    }

    private void LoadTeacherScores()
    {
        Оценки.Clear();
        using var dbContext = new StepAcademyDB(Program.DbContextOptions);

        var нагрузки = dbContext.Нагрузка.Where(
            x => x.IdПреподавателя == Program.CurrentUser.Id
        ).ToList();

        List<Models.ГруппаСтудентов> groupSt = new List<ГруппаСтудентов>();

        foreach (var item in нагрузки)
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

        foreach (var item in groupSt)
        {
            students.AddRange(dbContext.Студенты.Where(
                    x => x.IdГруппы == item.Id
                ).ToList()
            );
        }

        foreach (var item in students)
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
                    ИдОценки = ocen.Id,
                    Оценка = ocen.Балл
                }
            );
        }
    }

    private void SaveChanges()
    {
        var dialogService = new UserDialogService();

        try
        {

            using var dbContext = new StepAcademyDB(Program.DbContextOptions);
            foreach (var оценка in Оценки)
            {
                var dbScore = dbContext.Оценки.FirstOrDefault(о => о.Id == оценка.ИдОценки);
                if (dbScore!= default)
                {
                    dbScore.Балл = оценка.Оценка;
                }
            }

            dbContext.SaveChanges();
            dialogService.ShowMessageInfo("Уведомление", "Оценки были успешно сохранены!");

        }
        catch (Exception exception)
        {
            dialogService.ShowMessageError("Ошиибка", exception.ToString());
        }

    }

    private void CancelChanges()
    {
        LoadTeacherScores();
    }
}