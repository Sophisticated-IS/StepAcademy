using System.Collections.ObjectModel;

namespace StepAcademyApp.ViewModels;

internal sealed class StudentScheduleVM
{
    internal sealed class MockSchedule
    {
        public string Начало { get; set; }
        public string Конец { get; set; }
        public string ФИОПрепода { get; set; }
        public string ТипЗанятия { get; set; }
        public string Предмет { get; set; }
    }

    public ObservableCollection<MockSchedule> Расписание { get; } = new();

    public StudentScheduleVM()
    {
        Расписание.Add(
            new MockSchedule()
            {
                Начало = "8:00",
                Конец = "9:00",
                ФИОПрепода = "Бурунов Олег Иванович",
                ТипЗанятия = "Лекция",
                Предмет = "Информатика"
            }
        );
        
        Расписание.Add(
            new MockSchedule()
            {
                Начало = "9:00",
                Конец = "10:00",
                ФИОПрепода = "Бурунов Олег Иванович",
                ТипЗанятия = "Лабораторная",
                Предмет = "Информатика"
            }
        );
        
        Расписание.Add(
            new MockSchedule()
            {
                Начало = "10:30",
                Конец = "12:00",
                ФИОПрепода = "Гаврилов Михаил Анатольевич",
                ТипЗанятия = "Лекция",
                Предмет = "История"
            }
        );
    }
}