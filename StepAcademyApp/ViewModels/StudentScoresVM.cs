using System.Collections.ObjectModel;


namespace StepAcademyApp.ViewModels;

internal sealed class StudentScoresVM
{
    public ObservableCollection<MockClass> Оценки { get; } = new();

    internal sealed class MockClass
    {
        public string Специальность { get; set; }
        public string Отделение { get; set; }
        public string НазваниеПредмета { get; set; }
        public string Оценка { get; set; }
    } 
    public StudentScoresVM()
    {
        Оценки.Add(new MockClass
        {
            Специальность = "Информатика",
            Отделение = "ИСТИ",
            НазваниеПредмета = "Основы программирования",
            Оценка = "5"
        });
        Оценки.Add(new MockClass
        {
            Специальность = "Информатика",
            Отделение = "ИСТИ",
            НазваниеПредмета = "Основы программирования",
            Оценка = "4"
        });
        
        Оценки.Add(new MockClass
        {
            Специальность = "Информатика",
            Отделение = "ИСТИ",
            НазваниеПредмета = "Основы программирования",
            Оценка = "3"
        });

        Оценки.Add(new MockClass
        {
            Специальность = "Информатика",
            Отделение = "ИСТИ",
            НазваниеПредмета = "Основы программирования",
            Оценка = "2"
        });
        
        Оценки.Add(new MockClass
        {
            Специальность = "Информатика",
            Отделение = "ИСТИ",
            НазваниеПредмета = "Основы программирования",
            Оценка = "1"
        });
        
        Оценки.Add(new MockClass
        {
            Специальность = "Информатика",
            Отделение = "ИСТИ",
            НазваниеПредмета = "Основы программирования",
            Оценка = "0"
        });
    }
}