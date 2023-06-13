using System;

namespace StepAcademyApp.Models;

internal sealed class Студент : Гражданин
{
    /// <summary>
    /// null - студент без группы
    /// </summary>
    public uint? IdГруппы { get; set; }
    public ГруппаСтудентов Группа { get; set; }
    
    public static Студент FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');
        Студент Студент = new Студент();
        Студент.Id = (uint)Convert.ToUInt32(values[0]);
        Студент.СерияНомерПаспорта = (uint)Convert.ToUInt32(values[1]);
        Студент.Имя = values[2];
        Студент.Фамилия = values[3];
        Студент.Отчество = values[4];
        Студент.ДатаРождения = new DateTime(System.DateTime.Now.Ticks + 1, DateTimeKind.Utc); //Convert.ToDateTime(values[5]);
        Студент.IdГруппы = (uint)Convert.ToUInt32(values[6]);
        return Студент;
    }
}