using Avalonia;
using Avalonia.ReactiveUI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using StepAcademyApp.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using StepAcademyApp.Models;
using SharpHash.Base;
using System.Text;
using System.IO;

namespace StepAcademyApp
{
    class Program
    {
        public static DbContextOptions DbContextOptions;
        public static Гражданин CurrentUser { get; set; }

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            DataBaseInit();
            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        }
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                         .UsePlatformDetect()
                         .LogToTrace()
                         .UseReactiveUI();

        public static void FillDbCsv(DataBase.StepAcademyDB dbContext)
        {
            if(dbContext.Database.EnsureCreated()){
                List<Models.Отделение> listOtdel = File.ReadAllLines("csv\\Отделение.csv")
                                            .Skip(1)
                                            .Select(v => Models.Отделение.FromCsv(v))
                                            .ToList();
                List<Models.Специальность> listSpecial = File.ReadAllLines("csv\\Специальность.csv")
                                            .Skip(1)
                                            .Select(v => Models.Специальность.FromCsv(v))
                                            .ToList();
                List<Models.Предмет> listPredmet = File.ReadAllLines("csv\\Предмет.csv")
                                            .Skip(1)
                                            .Select(v => Models.Предмет.FromCsv(v))
                                            .ToList();
                List<Models.ТипЗанятия> listTipZan = File.ReadAllLines("csv\\ТипЗанятия.csv")
                                            .Skip(1)
                                            .Select(v => Models.ТипЗанятия.FromCsv(v))
                                            .ToList();
                
                List<Models.Преподаватель> listPrepod = File.ReadAllLines("csv\\Преподаватель.csv")
                                            .Skip(1)
                                            .Select(v => Models.Преподаватель.FromCsv(v))
                                            .ToList();
                List<Models.ГруппаСтудентов> listGrup = new List<Models.ГруппаСтудентов>();

                List<Models.ОплатаЗанятия> listOplataZan = new List<Models.ОплатаЗанятия>();
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
                }
                List<Models.Зарплата> listZarplat = File.ReadAllLines("csv\\Зарплата.csv")
                                            .Skip(1)
                                            .Select(v => Models.Зарплата.FromCsv(v))
                                            .ToList();
                for (int i = 0; i < 100; i++)
                {
                    listZarplat[i].Преподаватель = listPrepod[i];
                }
                List<Models.Нагрузка> listNagruzka = new List<Models.Нагрузка>();
                for (int i = 0; i < 100; i++)
                {
                    var dt = DateTime.Now;
                    var startDt = dt.AddMonths(-dt.Month).AddMonths(i % 12).ToUniversalTime();
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
                            ВремяНачалаЗанятия = startDt,
                            ВремяКонцаЗанятия = startDt.AddHours(1.5),
                        }
                    );
                }
                List<Models.УчетныеДанные> listUchetDat = new List<Models.УчетныеДанные>();
                for (int i = 0; i < 100; i++)
                {
                    listUchetDat.Add(
                        new Models.УчетныеДанные()
                        {
                            Гражданин = listPrepod[i],
                            Логин = "teacher" + i,
                            Пароль = HashFactory.Crypto.CreateGOST3411_2012_512().ComputeString("teacher" + i, Encoding.UTF8).ToString(),
                            Соль = "teacherHorosh" + i
                        }
                    );
                }
                List<Models.Студент> listStudents = File.ReadAllLines("csv\\Студенты.csv")
                                            .Skip(1)
                                            .Select(v => Models.Студент.FromCsv(v))
                                            .ToList();
                List<Models.Оценка> listOcen = new List<Models.Оценка>();
                for(int i = 0; i < 100; i++)
                {
                    listStudents[i].Группа = listGrup[(int)listStudents[i].IdГруппы - 1];
                    listOcen.Add(
                        new Models.Оценка
                        {
                            Id = (uint)(i + 1),
                            IdСтудента = listStudents[i].Id,
                            Студент = listStudents[i],
                            IdГруппы = listStudents[i].IdГруппы.HasValue ? listStudents[i].IdГруппы.Value : default,
                            Группа = listStudents[i].Группа,
                            IdПредмета = listPredmet[i % 10].Id,
                            Предмет = listPredmet[i % 10],
                            Балл = (short)((100 + i + 1) % 100),
                        }
                    );
                    listUchetDat.Add(
                        new Models.УчетныеДанные()
                        {
                            Гражданин = listStudents.Last(),
                            Логин = "student" + $"{i + 1}",
                            Пароль = HashFactory.Crypto.CreateGOST3411_2012_512().ComputeString("student"  + $"{i + 1}", Encoding.UTF8).ToString(),
                            Соль = "studentHorosh" + $"{i + 1}"
                        }
                    );
                }
                listPrepod.Add(
                    new Models.Преподаватель
                    {
                        Id = (uint)(222),
                        СерияНомерПаспорта = (uint)(222),
                        Имя = "Name ",
                        Фамилия = "SurName ",
                        Отчество = "FName ",
                        ДатаРождения = new DateTime(System.DateTime.Now.Ticks + 1, DateTimeKind.Utc),
                        НомерТелефона = $"8980980980",
                        Стаж = (TimeSpan)(DateTime.SpecifyKind(new DateTime(2010 + 1, 1, 1, 8, 0, 0), DateTimeKind.Utc) - DateTime.SpecifyKind(new DateTime(2010, 1, 1, 8, 0, 0), DateTimeKind.Utc)),
                        Админ = true
                    }
                );
                listUchetDat.Add(
                    new Models.УчетныеДанные() { 
                        Гражданин = listPrepod.LastOrDefault(),
                        Логин = "admin",
                        Пароль = HashFactory.Crypto.CreateGOST3411_2012_512().ComputeString("admin", Encoding.UTF8).ToString(),
                        Соль = "teacherHorosh"
                    }
                );
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
                dbContext.Студенты.AddRange(
                        listStudents
                            );
                dbContext.УчетныеДанные.AddRange(
                        listUchetDat
                            );
                dbContext.Оценки.AddRange(
                        listOcen
                            );        
                dbContext.SaveChanges();
            }
        }

        public static void DataBaseInit()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=StepAcademyDB;Username=postgres;Password=postgres");
            var dbContext = new StepAcademyDB(optionsBuilder.Options);
            DbContextOptions = optionsBuilder.Options;
            dbContext.Database.EnsureDeleted();
            FillDbCsv(dbContext);
            if (dbContext.Database.EnsureCreated())
            {
                List<Models.Отделение> listOtdel = new List<Models.Отделение>();
                List<Models.Специальность> listSpecial = new List<Models.Специальность>();
                List<Models.Предмет> listPredmet = new List<Models.Предмет>();
                List<Models.ТипЗанятия> listTipZan = new List<Models.ТипЗанятия>();
                for (int i = 0; i < 10; i++)
                {
                    listOtdel.Add(new Models.Отделение
                    {
                        Id = (uint)(i + 1),
                        Название = "Название отделения " + i
                    });
                    listSpecial.Add(new Models.Специальность
                    {
                        Id = (uint)(i + 1),
                        Название = "Название специальности " + i
                    });
                    listPredmet.Add(new Models.Предмет
                    {
                        Id = (uint)(i + 1),
                        Название = "Название предмета " + i
                    });
                    listTipZan.Add(new Models.ТипЗанятия
                    {
                        Id = (uint)(i + 1),
                        Название = "Название занятия " + i
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
                List<Models.УчетныеДанные> listUchetDat = new List<Models.УчетныеДанные>();
                listPrepod.Add(
                        new Models.Преподаватель
                        {
                            Id = (uint)(1),
                            СерияНомерПаспорта = (uint)(1),
                            Имя = "Имя " + (1),
                            Фамилия = "Фамилия " + (1),
                            Отчество = "Отчество " + (1),
                            ДатаРождения = new DateTime(System.DateTime.Now.Ticks + 1, DateTimeKind.Utc),
                            НомерТелефона = $"89{1}",
                            Стаж = (TimeSpan)(DateTime.SpecifyKind(new DateTime(2010 + 1, 1, 1, 8, 0, 0), DateTimeKind.Utc) - DateTime.SpecifyKind(new DateTime(2010, 1, 1, 8, 0, 0), DateTimeKind.Utc)),
                            Админ = true
                        }
                        );
                listUchetDat.Add(
                    new Models.УчетныеДанные() { 
                        Гражданин = listPrepod.LastOrDefault(),
                        Логин = "teacherAdmin",
                        Пароль = HashFactory.Crypto.CreateGOST3411_2012_512().ComputeString("admin", Encoding.UTF8).ToString(),
                        Соль = "teacherHorosh"
                    }
                    );
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
                    //if (i != 99)
                        listPrepod.Add(
                            new Models.Преподаватель
                            {
                                Id = (uint)(i + 2),
                                СерияНомерПаспорта = (uint)(i + 2),
                                Имя = "Имя " + (i + 1),
                                Фамилия = "Фамилия " + (i + 1),
                                Отчество = "Отчество " + (i + 1),
                                ДатаРождения = new DateTime(System.DateTime.Now.Ticks + i + 1, DateTimeKind.Utc),
                                НомерТелефона = $"89{i + 1}",
                                Стаж = (TimeSpan)(DateTime.SpecifyKind(new DateTime(2010 + i + 1, 1, 1, 8, 0, 0), DateTimeKind.Utc) - DateTime.SpecifyKind(new DateTime(2010, 1, 1, 8, 0, 0), DateTimeKind.Utc)),
                                Админ = false
                            }
                        );
                    //if (i != 99)
                        listUchetDat.Add(
                            new Models.УчетныеДанные()
                            {
                                Гражданин = listPrepod[i + 1],
                                Логин = "teacher" + i,
                                Пароль = HashFactory.Crypto.CreateGOST3411_2012_512().ComputeString("teacher" + i, Encoding.UTF8).ToString(),
                                Соль = "teacherHorosh" + i
                            }
                        );
                    listZarplat.Add(
                        new Models.Зарплата
                        {
                            Id = (uint)(i + 1),
                            IdУчитель = listPrepod[i + 1].Id,
                            Преподаватель = listPrepod[i + 1],
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
                    var dt = DateTime.Now;
                    var startDt = dt.AddMonths(-dt.Month).AddMonths(i % 12).ToUniversalTime();
                    listNagruzka.Add(
                        new Models.Нагрузка
                        {
                            Id = (uint)(i + 1),
                            IdГруппы = listGrup[i].Id,
                            Группа = listGrup[i],
                            IdПредмета = listPredmet[i % 10].Id,
                            Предмет = listPredmet[i % 10],
                            IdПреподавателя = listPrepod[i + 1].Id,
                            Преподаватель = listPrepod[i + 1],
                            IdТипЗанятия = listTipZan[i % 10].Id,
                            ТипЗанятия = listTipZan[i % 10],
                            ВремяНачалаЗанятия = startDt,
                            ВремяКонцаЗанятия = startDt.AddHours(1.5),
                        }
                        );
                }
                //listPrepod.RemoveAt(listPrepod.Count - 1);
                //listUchetDat.RemoveAt(listUchetDat.Count - 1);
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
                var studId = 110u;
                List<Models.Студент> listStudents = new List<Models.Студент>();
                List<Models.Оценка> listOcen = new List<Models.Оценка>();
                listStudents.Add(
                            new Models.Студент
                            {
                                Id = studId,
                                СерияНомерПаспорта = (uint)(studId + 100000),
                                Имя = "Имя " + studId,
                                Фамилия = "Фамилия " + studId,
                                Отчество = "Отчество " + studId,
                                ДатаРождения = new DateTime(System.DateTime.Now.Ticks + 1, DateTimeKind.Utc),
                                IdГруппы = listGrup[1].Id,
                                Группа = listGrup[1]
                            }
                            );
                listUchetDat.Add(
                    new Models.УчетныеДанные()
                    {
                        Гражданин = listStudents.LastOrDefault(),
                        Логин = "student",
                        Пароль = HashFactory.Crypto.CreateGOST3411_2012_512().ComputeString("student", Encoding.UTF8).ToString(),
                        Соль = "studentHorosh"
                    }
                    );
                listOcen.Add(
                    new Models.Оценка
                    {
                        Id = (uint)(studId),
                        IdСтудента = listStudents.Last().Id,
                        Студент = listStudents.Last(),
                        IdГруппы = listStudents.Last().IdГруппы.HasValue ? listStudents.Last().IdГруппы.Value : default,
                        Группа = listStudents.Last().Группа,
                        IdПредмета = listPredmet[1].Id,
                        Предмет = listPredmet[1],
                        Балл = (short)(1),
                    }
                    );
                studId++;
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 1000; j++, studId++)
                    {
                        if (!(i == 99 && j == 999))
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
                                    Id = studId,
                                    IdСтудента = listStudents.Last().Id,
                                    Студент = listStudents.Last(),
                                    IdГруппы = listStudents.Last().IdГруппы.HasValue ? listStudents.Last().IdГруппы.Value : default,
                                    Группа = listStudents.Last().Группа,
                                    IdПредмета = listPredmet[j % 10].Id,
                                    Предмет = listPredmet[j % 10],
                                    Балл = (short)(j + 1),
                                }
                                );
                            listUchetDat.Add(
                                new Models.УчетныеДанные()
                                {
                                    Гражданин = listStudents.Last(),
                                    Логин = "student" + (1000 * i) + j + 1,
                                    Пароль = HashFactory.Crypto.CreateGOST3411_2012_512().ComputeString("student" + (1000 * i) + j + 1, Encoding.UTF8).ToString(),
                                    Соль = "studentHorosh" + (1000 * i) + j + 1
                                }
                            );
                        }
                    }

                }
                //listStudents.RemoveAt(listStudents.Count - 1);
                //listUchetDat.RemoveAt(listUchetDat.Count - 1);
                dbContext.Студенты.AddRange(
                        listStudents
                            );
                dbContext.УчетныеДанные.AddRange(
                        listUchetDat
                            );
                dbContext.Оценки.AddRange(
                        listOcen
                            );
                dbContext.SaveChanges();
            }
        }
    }
}