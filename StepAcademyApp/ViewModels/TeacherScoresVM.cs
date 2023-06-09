﻿using System.Collections.ObjectModel;
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

        Оценки.Add(new MockClass
        {
            ФИОСтудента = "Иванов Иван Иванович",
            Специальность = "Информатика",
            Отделение = "ИСТИ",
            НазваниеПредмета = "Основы программирования",
            Оценка = "5"
        });
        Оценки.Add(new MockClass
        {
            ФИОСтудента = "Баранов Андрей Викторович",
            Специальность = "Информатика",
            Отделение = "ИСТИ",
            НазваниеПредмета = "Основы программирования",
            Оценка = "4"
        });
        
        Оценки.Add(new MockClass
        {
            ФИОСтудента = "Долгих Игорь Анатольевич",
            Специальность = "Информатика",
            Отделение = "ИСТИ",
            НазваниеПредмета = "Основы программирования",
            Оценка = "3"
        });

        Оценки.Add(new MockClass
        {
            ФИОСтудента = "Жданов Арсений Викторович",
            Специальность = "Информатика",
            Отделение = "ИСТИ",
            НазваниеПредмета = "Основы программирования",
            Оценка = "2"
        });
        
        Оценки.Add(new MockClass
        {
            ФИОСтудента = "Сальцев Иван Иванович",
            Специальность = "Информатика",
            Отделение = "ИСТИ",
            НазваниеПредмета = "Основы программирования",
            Оценка = "1"
        });
        
        Оценки.Add(new MockClass
        {
            ФИОСтудента = "Новак Иван Иванович",
            Специальность = "Информатика",
            Отделение = "ИСТИ",
            НазваниеПредмета = "Основы программирования",
            Оценка = "0"
        });
    }
}