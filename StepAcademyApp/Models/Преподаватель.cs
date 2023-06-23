using System;

namespace StepAcademyApp.Models;

public sealed class Преподаватель : Гражданин
{
    public string НомерТелефона  { get; set; }
    public bool Админ { get; set; }
    public TimeSpan Стаж { get; set; }

    public static Преподаватель FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');
        Преподаватель Преподаватель = new Преподаватель();
        Преподаватель.Id = (uint)Convert.ToUInt32(values[0]);
        Преподаватель.СерияНомерПаспорта = (uint)Convert.ToUInt32(values[1]);
        Преподаватель.Имя = values[2];
        Преподаватель.Фамилия = values[3];
        Преподаватель.Отчество = values[4];
        Преподаватель.ДатаРождения = new DateTime(System.DateTime.Now.Ticks + 1, DateTimeKind.Utc); //Convert.ToDateTime(values[5]);
        Преподаватель.НомерТелефона = values[6];
        Преподаватель.Админ = Convert.ToBoolean(values[7]);
        Преподаватель.Стаж = (TimeSpan)(DateTime.SpecifyKind(new DateTime(2010 + 1, 1, 1, 8, 0, 0), DateTimeKind.Utc) - DateTime.SpecifyKind(new DateTime(2010, 1, 1, 8, 0, 0), DateTimeKind.Utc));
        return Преподаватель;
    }
}