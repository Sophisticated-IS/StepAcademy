using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using StepAcademyApp.DataBase;

namespace StepAcademyApp.ViewModels;

internal sealed class StudentScheduleVM : ViewModelBase
{
    internal sealed class StudSchedule
    {
        public string Начало { get; set; }
        public string Конец { get; set; }
        public string ФИОПрепода { get; set; }
        public string ТипЗанятия { get; set; }
        public string Предмет { get; set; }
    }

    public ObservableCollection<StudSchedule> Расписание { get; } = new();

    [Reactive]
    public string CurrentDateTime { get; set; }

    [Reactive]
    public int PageNumberDay { get; set; }
    public StudentScheduleVM()
    {
        this.WhenAnyValue(x => x.PageNumberDay).Subscribe(x =>
        {
            if (x<=0)
            {
                PageNumberDay = 1;
            }
            else
            {
                var dt = DateTime.Now;
                CurrentDateTime = dt.AddDays(-dt.Day).AddDays(PageNumberDay).ToString("dddd, dd MMMM, yyyy");
                using var dbContext = new StepAcademyDB(Program.DbContextOptions);
                var student = dbContext.Студенты.First(x=>x.Id == Program.CurrentUser.Id);
                var schedule = dbContext.Нагрузка
                                        .Where(x => x.IdГруппы == student.IdГруппы
                                        && x.ВремяНачалаЗанятия.Date.Day == PageNumberDay
                                        && x.ВремяКонцаЗанятия.Date.Month == DateTime.Now.Month
                                        && x.ВремяКонцаЗанятия.Date.Year == DateTime.Now.Year)
                                        .Include(x=>x.Преподаватель)
                                        .Include(x=>x.ТипЗанятия)
                                        .Include(x=>x.Предмет)
                                        .ToList();
                Расписание.Clear();
                foreach (var item in schedule)
                {
                    Расписание.Add(new StudSchedule
                    {
                        Начало = item.ВремяНачалаЗанятия.TimeOfDay.ToString(),
                        Конец = item.ВремяКонцаЗанятия.TimeOfDay.ToString(),
                        ФИОПрепода = item.Преподаватель.Имя + " " + item.Преподаватель.Фамилия + " " + item.Преподаватель.Отчество,
                        ТипЗанятия = item.ТипЗанятия.Название,
                        Предмет = item.Предмет.Название
                        
                    });
                }

            }
            
        });
        
        PageNumberDay = 1;

    }
    
}