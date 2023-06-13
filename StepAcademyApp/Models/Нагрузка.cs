using System;

namespace StepAcademyApp.Models;

internal sealed class Нагрузка
{
    public uint Id { get; set; }
    public uint IdПреподавателя { get; set; }
    public Преподаватель Преподаватель { get; set; }
    public uint IdГруппы { get; set; }
    public ГруппаСтудентов Группа { get; set; }
    public DateTime ВремяНачалаЗанятия { get; set; }
    public DateTime ВремяКонцаЗанятия { get; set; }
    public uint IdПредмета { get; set; }
    public Предмет Предмет { get; set; }
    public uint IdТипЗанятия { get; set; }
    public ТипЗанятия ТипЗанятия { get; set; }

    public static Нагрузка FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');
        Нагрузка Нагрузка = new Нагрузка();
        Нагрузка.Id = (uint)Convert.ToUInt32(values[0]);
        Нагрузка.IdПреподавателя = (uint)Convert.ToUInt32(values[1]);
        Нагрузка.IdГруппы = (uint)Convert.ToUInt32(values[2]);
        /*Random gen = new Random();
        DateTime dt = DateTime.ParseExact(values[3], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        dt = dt.AddHours(gen.Next(8,16)).AddMinutes(gen.Next(0,60));
        Нагрузка.ВремяНачалаЗанятия = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        Нагрузка.ВремяКонцаЗанятия = DateTime.SpecifyKind(dt.AddHours(1.5), DateTimeKind.Utc);*/
        Нагрузка.IdПредмета = (uint)Convert.ToInt32(values[3]);
        Нагрузка.IdТипЗанятия = (uint)Convert.ToInt32(values[4]);        
        return Нагрузка;
    }
}