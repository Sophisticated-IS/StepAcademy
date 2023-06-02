using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using StepAcademyApp.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StepAcademyApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5436;Database=StepAcademyDB;Username=postgres;Password=postgres");
            var dbContext = new StepAcademyDB(optionsBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            List<Models.Отделение> listOtdel = new List<Models.Отделение>();
            List<Models.Специальность> listSpecial = new List<Models.Специальность>();
            List<Models.Предмет> listPredmet = new List<Models.Предмет>();
            List<Models.ТипЗанятия> listTipZan = new List<Models.ТипЗанятия>();
            for (int i = 0; i < 10; i++)
            {
                listOtdel.Add(new Models.Отделение
                {
                    Id = (uint)(i + 1),
                    Name = "Название отделения " + i
                });
                listSpecial.Add(new Models.Специальность
                {
                    Id = (uint)(i + 1),
                    Name = "Название специальности " + i
                });
                listPredmet.Add(new Models.Предмет
                {
                    Id = (uint)(i + 1),
                    Name = "Название предмета " + i
                });
                listTipZan.Add(new Models.ТипЗанятия
                {
                    Id = (uint)(i + 1),
                    Name = "Название занятия " + i
                });
            }
            dbContext.Отделения.AddRange(
                    listOtdel
                    );
            dbContext.Специальности.AddRange(
                    listSpecial
                    );
            dbContext.Предметы.AddRange(
                    listPredmet
                    );
            dbContext.ТипЗанятий.AddRange(
                    listTipZan
                    );
            dbContext.SaveChanges();
            List<Models.Преподаватель> listPrepod = new List<Models.Преподаватель>();
            List<Models.ГруппаСтудентов> listGrup = new List<Models.ГруппаСтудентов>();
            List<Models.ОплатаЗанятия> listOplataZan = new List<Models.ОплатаЗанятия>();
            List<Models.Зарплата> listZarplat = new List<Models.Зарплата>();
            List<Models.Нагрузка> listNagruzka = new List<Models.Нагрузка>();
            for (int i = 0; i < 100; i++)
            {
                listOplataZan.Add(
                    new Models.ОплатаЗанятия
                    {
                        Id = (uint)(i + 1),
                        IdПредмет = listPredmet[i % 10].Id,
                        Предмет = listPredmet[i % 10],
                        IdТипЗанятия = listTipZan[i % 10].Id,
                        ТипЗанятия = listTipZan[i % 10],
                        ЧасоваяОплата = (decimal)((i + 1) * 100.1),
                    }
                    );
                listPrepod.Add(
                    new Models.Преподаватель
                    {
                        Id = (uint)(i + 1),
                        СерияНомерПаспорта = (uint)(i + 1),
                        Имя = "Имя " + (i + 1),
                        Фамилия = "Фамилия " + (i + 1),
                        Отчество = "Отчество " + (i + 1),
                        ДатаРождения = new DateTime(System.DateTime.Now.Ticks + i, DateTimeKind.Utc),
                        НомерТелефона = $"89{i}",
                        Стаж = (TimeSpan)(DateTime.SpecifyKind(new DateTime(2010 + i, 1, 1, 8, 0, 0), DateTimeKind.Utc) - DateTime.SpecifyKind(new DateTime(2010, 1, 1, 8, 0, 0), DateTimeKind.Utc))
                    }
                    );
                listZarplat.Add(
                    new Models.Зарплата
                    {
                        Id = (uint)(i + 1),
                        IdУчитель = listPrepod[i].Id,
                        Преподаватель = listPrepod[i],
                        Year = (uint)(i + 1),
                        Month = (uint)(i + 1),
                        СуммаВыплат = (decimal)((i + 1) * 100.1),
                    }
                    );
                listGrup.Add(
                    new Models.ГруппаСтудентов
                    {
                        Id = (uint)(i + 1),
                        ОтделениеId = listOtdel[i % 10].Id,
                        Отделение = listOtdel[i % 10],
                        СпециальностьId = listSpecial[i % 10].Id,
                        Специальность = listSpecial[i % 10],
                    }
                    );
                listNagruzka.Add(
                    new Models.Нагрузка
                    {
                        Id = (uint)(i + 1),
                        IdГруппы = listGrup[i].Id,
                        Группа = listGrup[i],
                        IdПредмета = listPredmet[i % 10].Id,
                        Предмет = listPredmet[i % 10],
                        IdПреподавателя = listPrepod[i].Id,
                        Преподаватель = listPrepod[i],
                        IdТипЗанятия = listTipZan[i % 10].Id,
                        ТипЗанятия = listTipZan[i % 10],
                        КолвоЧасов = (uint)(i + 100),
                    }
                    );
            }
            dbContext.ОплатаЗанятий.AddRange(
                listOplataZan
                    );
            dbContext.Учителя.AddRange(
                listPrepod
                    );
            dbContext.Зарплаты.AddRange(
                listZarplat
                    );
            dbContext.ГруппыСтудентов.AddRange(
                listGrup
                );
            dbContext.Нагрузка.AddRange(
                    listNagruzka
                    );
            dbContext.SaveChanges();
            var studId = 101u;
            List<Models.Студент> listStudents = new List<Models.Студент>();
            List<Models.Оценка> listOcen = new List<Models.Оценка>();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 1000; j++,studId++)
                {
                    listStudents.Add(
                        new Models.Студент
                        {
                            Id = studId,
                            СерияНомерПаспорта = (uint)(((1000 * i) + j + 1) + 100000),
                            Имя = "Имя " + (1000 * i) + j + 1,
                            Фамилия = "Фамилия " + (1000 * i) + j + 1,
                            Отчество = "Отчество " + (1000 * i) + j + 1,
                            ДатаРождения = new DateTime(System.DateTime.Now.Ticks + i, DateTimeKind.Utc),
                            IdГруппы = listGrup[i].Id,
                            Группа = listGrup[i]
                        }
                        );
                    listOcen.Add(
                        new Models.Оценка
                        {
                            Id = (uint)((1000 * i) + j + 1),
                            IdСтудента = listStudents[(1000 * i) + j].Id,
                            Студент = listStudents[(1000 * i) + j],
                            IdГруппы = listStudents[(1000 * i) + j].IdГруппы.HasValue ? listStudents[(1000 * i) + j].IdГруппы.Value : default,
                            Группа = listStudents[(1000 * i) + j].Группа,
                            IdПредмета = listPredmet[j % 10].Id,
                            Предмет = listPredmet[j % 10],
                            Балл = (short)(j + 1),
                        }
                        );
                }
                                              
            }
            dbContext.Студенты.AddRange(
                    listStudents
                        );
            dbContext.Оценки.AddRange(
                    listOcen
                        );
            dbContext.SaveChanges();
        }
    }
}