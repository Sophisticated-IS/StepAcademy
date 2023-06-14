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

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

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
                List<Models.Нагрузка> listNagruzka = File.ReadAllLines("csv\\nagruzka1.csv")
                                            .Skip(1)
                                            .Select(v => Models.Нагрузка.FromCsv(v))
                                            .ToList();
                listNagruzka.AddRange(File.ReadAllLines("csv\\nagruzka2.csv")
                                            .Skip(1)
                                            .Select(v => Models.Нагрузка.FromCsv(v))
                                            .ToList());
                listNagruzka.AddRange(File.ReadAllLines("csv\\nagruzka3.csv")
                                            .Skip(1)
                                            .Select(v => Models.Нагрузка.FromCsv(v))
                                            .ToList());
                listNagruzka.AddRange(File.ReadAllLines("csv\\nagruzka4.csv")
                                            .Skip(1)
                                            .Select(v => Models.Нагрузка.FromCsv(v))
                                            .ToList());
                listNagruzka.AddRange(File.ReadAllLines("csv\\nagruzka5.csv")
                                            .Skip(1)
                                            .Select(v => Models.Нагрузка.FromCsv(v))
                                            .ToList());
                listNagruzka.AddRange(File.ReadAllLines("csv\\nagruzka6.csv")
                                            .Skip(1)
                                            .Select(v => Models.Нагрузка.FromCsv(v))
                                            .ToList());
                listNagruzka.AddRange(File.ReadAllLines("csv\\nagruzka7.csv")
                                            .Skip(1)
                                            .Select(v => Models.Нагрузка.FromCsv(v))
                                            .ToList());
                listNagruzka.AddRange(File.ReadAllLines("csv\\nagruzka8.csv")
                                            .Skip(1)
                                            .Select(v => Models.Нагрузка.FromCsv(v))
                                            .ToList());
                listNagruzka.AddRange(File.ReadAllLines("csv\\nagruzka9.csv")
                                            .Skip(1)
                                            .Select(v => Models.Нагрузка.FromCsv(v))
                                            .ToList());
                listNagruzka.AddRange(File.ReadAllLines("csv\\nagruzka10.csv")
                                            .Skip(1)
                                            .Select(v => Models.Нагрузка.FromCsv(v))
                                            .ToList());
                for (int i = 0; i < 9000; i++)
                {
                    listNagruzka[i].Группа = listGrup[(int)listNagruzka[i].IdГруппы - 1];
                    listNagruzka[i].Предмет = listPredmet[(int)listNagruzka[i].IdПредмета - 1];
                    listNagruzka[i].Преподаватель = listPrepod[(int)listNagruzka[i].IdПреподавателя - 1];
                    listNagruzka[i].ТипЗанятия = listTipZan[(int)listNagruzka[i].IdТипЗанятия - 1];
                    Random gen = new Random();
                    int dd = i / 100;
                    int day = dd % 30 == 0 ? 30 : dd % 30;
                    string date = day > 9 ? $"2023-06-{day}" : $"2023-06-0{day}";
                    DateTime dt = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    dt = dt.AddHours(8 + 2 * (dd / 30 % 3)).AddMinutes(gen.Next(0, 3) * 10);
                    listNagruzka[i].ВремяНачалаЗанятия = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                    listNagruzka[i].ВремяКонцаЗанятия = DateTime.SpecifyKind(dt.AddHours(1.5), DateTimeKind.Utc);
                }
                List<Models.УчетныеДанные> listUchetDat = new List<Models.УчетныеДанные>();
                for (int i = 0; i < 100; i++)
                {
                    string login = "teacher" + $"{i + 1}";
                    string salt = RandomString(128);
                    string password = HashFactory.Crypto.CreateGOST3411_2012_512().ComputeString(login + salt, Encoding.UTF8).ToString();
                    listUchetDat.Add(
                        new Models.УчетныеДанные()
                        {
                            Гражданин = listPrepod[i],
                            Логин = login,
                            Пароль = password,
                            Соль = salt
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
                    string login = "student" + $"{i + 1}";
                    string salt = RandomString(128);
                    string password = HashFactory.Crypto.CreateGOST3411_2012_512().ComputeString(login + salt, Encoding.UTF8).ToString();
                    listUchetDat.Add(
                        new Models.УчетныеДанные()
                        {
                            Гражданин = listStudents[i],
                            Логин = login,
                            Пароль = password,
                            Соль = salt
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
                string saltAdmin = RandomString(128);
                listUchetDat.Add(
                    new Models.УчетныеДанные() { 
                        Гражданин = listPrepod.LastOrDefault(),
                        Логин = "admin",
                        Пароль = HashFactory.Crypto.CreateGOST3411_2012_512().ComputeString("admin" + saltAdmin, Encoding.UTF8).ToString(),
                        Соль = saltAdmin
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
            //dbContext.Database.EnsureDeleted();
            FillDbCsv(dbContext);
        }
    }
}