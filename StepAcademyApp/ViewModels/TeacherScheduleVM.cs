﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using StepAcademyApp.DataBase;
using StepAcademyApp.Models;
using StepAcademyApp.Services;
using StepAcademyApp.Views.Application;

namespace StepAcademyApp.ViewModels;

internal sealed class TeacherScheduleVM : ViewModelBase
{
    internal sealed class TeachSchedule
    {
        public uint IdНагрузки { get; set; }
        public uint IdГруппы { get; set; }
        public string Начало { get; set; }
        public string Конец { get; set; }
        public string ФИОПрепода { get; set; }
        public string ТипЗанятия { get; set; }
        public string Предмет { get; set; }
    }

    public ObservableCollection<TeachSchedule> Расписание { get; } = new();

    public ReactiveCommand<Unit, Unit> RemoveClassCommand { get; }
    public ReactiveCommand<Unit, Unit> AddClassCommand { get; }

    [Reactive]
    public string CurrentDateTime { get; set; }

    public bool IsTeacherAdmin { get; set; }


    public TeachSchedule? SelectedClass { get; set; }

    [Reactive]
    public int PageNumberDay { get; set; }

    public TeacherScheduleVM()
    {
        if (Program.CurrentUser is not Преподаватель преподаватель) return;

        IsTeacherAdmin = преподаватель.Админ;

        RemoveClassCommand = ReactiveCommand.Create(RemoveClass);
        AddClassCommand = ReactiveCommand.CreateFromTask(AddClass);


        Func<Нагрузка, int, bool> func;
        if (IsTeacherAdmin)
        {
            func = (x, i) => x.ВремяНачалаЗанятия.Date.Day == PageNumberDay
                             && x.ВремяКонцаЗанятия.Date.Month == DateTime.Now.Month
                             && x.ВремяКонцаЗанятия.Date.Year == DateTime.Now.Year;
        }
        else
        {
            func = (x, i) => x.IdПреподавателя == Program.CurrentUser.Id
                             && x.ВремяНачалаЗанятия.Date.Day == PageNumberDay
                             && x.ВремяКонцаЗанятия.Date.Month == DateTime.Now.Month
                             && x.ВремяКонцаЗанятия.Date.Year == DateTime.Now.Year;
        }

        this.WhenAnyValue(x => x.PageNumberDay).Subscribe(x =>
        {
            if (x <= 0)
            {
                PageNumberDay = 1;
            }

            var dt = DateTime.Now;
            CurrentDateTime = dt.AddDays(-dt.Day).AddDays(PageNumberDay).ToString("dddd, dd MMMM, yyyy");
            using var dbContext = new StepAcademyDB(Program.DbContextOptions);
            var schedule = dbContext.Нагрузка
                                    .Include(x => x.Преподаватель)
                                    .Include(x => x.ТипЗанятия)
                                    .Include(x => x.Предмет)
                                    .Where(func)
                                    .ToList();
            Расписание.Clear();
            foreach (var item in schedule)
            {
                Расписание.Add(new TeacherScheduleVM.TeachSchedule()
                {
                    IdНагрузки = item.Id,
                    IdГруппы = item.IdГруппы,
                    Начало = item.ВремяНачалаЗанятия.TimeOfDay.ToString(),
                    Конец = item.ВремяКонцаЗанятия.TimeOfDay.ToString(),
                    ФИОПрепода =
                        item.Преподаватель.Имя + " " + item.Преподаватель.Фамилия + " " + item.Преподаватель.Отчество,
                    ТипЗанятия = item.ТипЗанятия.Название,
                    Предмет = item.Предмет.Название
                });
            }
        });

        PageNumberDay = 1;
    }

    private async Task AddClass()
    {
        DateOnly.TryParse(CurrentDateTime, out var date);
        var window = new AddClassWindow();
        window.InputDateOnly = date;
        var teachSchedule = Расписание.FirstOrDefault();
        if (teachSchedule != null) 
        {
            window.IdGroup = teachSchedule.IdГруппы;
        }

        if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            await window.ShowDialog(desktop.MainWindow);
            if (window.Result != null)
            {
                Расписание.Add(
                    new TeachSchedule()
                    {
                        IdНагрузки = window.Result.Id,
                        Начало = window.Result.ВремяНачалаЗанятия.TimeOfDay.ToString(),
                        Конец = window.Result.ВремяКонцаЗанятия.TimeOfDay.ToString(),
                        ТипЗанятия = window.Result.ТипЗанятия.Название,
                        Предмет = window.Result.Предмет.Название,
                        ФИОПрепода = window.Result.Преподаватель.Имя + " " + window.Result.Преподаватель.Фамилия + " " + window.Result.Преподаватель.Отчество,
                    });
            }
        }
    }

    private void RemoveClass()
    {
        if (SelectedClass is null) return;

        try
        {
            using var dbContext = new StepAcademyDB(Program.DbContextOptions);

            var dialogService = new UserDialogService();
            dialogService.ShowMessageInfo("Удаление", $"Занятие было удалено успешно!");

            var load = dbContext.Нагрузка.FirstOrDefault(l => l.Id == SelectedClass.IdНагрузки);

            if (load != default)
            {
                dbContext.Нагрузка.Remove(load);
                dbContext.SaveChanges();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}