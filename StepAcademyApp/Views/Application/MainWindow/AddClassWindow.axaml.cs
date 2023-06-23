using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using StepAcademyApp.DataBase;
using StepAcademyApp.Models;

namespace StepAcademyApp.Views.Application;

public partial class AddClassWindow : Window
{
    public sealed class StudSchedule
    {
        public StudSchedule(Преподаватель препод)
        {
            Препод = препод;
        }

        public string ФИОПрепода =>
            Препод.Имя + " " + Препод.Фамилия + " " + Препод.Отчество;
        public Преподаватель Препод { get; }
    }
    
    public ReactiveCommand<Unit, Unit> AddClassCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public TimeSpan? SelectedStartTime { get; set; }
    public TimeSpan? SelectedEndTime { get; set; }
    
    public StudSchedule? SelectedTeacher { get; set; }
    public Предмет? SelectedSubject { get; set; }
    public ТипЗанятия? SelectedSubjType { get; set; }

    public ObservableCollection<StudSchedule> Teachers { get;} = new ObservableCollection<StudSchedule>();
    public ObservableCollection<Предмет> Subjects { get;  } = new();
    public ObservableCollection<ТипЗанятия> SubjectTypes { get;  } = new();


    public Нагрузка? Result { get; set; }
    public DateOnly InputDateOnly { get; set; }
    public uint IdGroup { get; set; }
    public AddClassWindow()
    {

        
        AddClassCommand = ReactiveCommand.Create(AddClass);
        CancelCommand = ReactiveCommand.Create(Cancel);

        using var dbContext = new StepAcademyDB(Program.DbContextOptions);

        foreach (var препод in dbContext.Учителя)
        {
            Teachers.Add(new StudSchedule(препод));
        }

        foreach (var subject in dbContext.Предметы)
        {
            Subjects.Add(subject);
        }
        

        foreach (var type in dbContext.ТипЗанятий)
        {
            SubjectTypes.Add(type);
        }

        DataContext = this;
        
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif    
    }

    private void Cancel()
    {
       Close();
    }

    private void AddClass()
    {
        if (SelectedStartTime == null || SelectedEndTime == null
                                      || !(SelectedStartTime <= SelectedEndTime)
                                      || SelectedTeacher == null || SelectedSubject == null || SelectedSubjType == null) return;
        try
        {
            using var dbContext = new StepAcademyDB(Program.DbContextOptions);

            var max = dbContext.Нагрузка.Max(n => n.Id);
            var tmp  = new Нагрузка
            {
                Id = max+1,
                IdПреподавателя = SelectedTeacher.Препод.Id,
                IdПредмета = SelectedSubject.Id,
                IdТипЗанятия = SelectedSubjType.Id,
                IdГруппы = IdGroup,
                
                Предмет = SelectedSubject,
                Преподаватель = SelectedTeacher.Препод,
                ТипЗанятия = SelectedSubjType,
                ВремяНачалаЗанятия =DateTime.SpecifyKind(InputDateOnly.ToDateTime(TimeOnly.FromTimeSpan(SelectedStartTime.Value)),DateTimeKind.Utc) ,
                ВремяКонцаЗанятия = DateTime.SpecifyKind(InputDateOnly.ToDateTime(TimeOnly.FromTimeSpan(SelectedEndTime.Value)),DateTimeKind.Utc),
            };

            
           
            Result = tmp;//todo fix??
            dbContext.Нагрузка.Add(tmp);
            dbContext.SaveChanges();
        }
        catch (Exception exception)
        {
            //ignored
        }
        finally
        {
            Close();
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}