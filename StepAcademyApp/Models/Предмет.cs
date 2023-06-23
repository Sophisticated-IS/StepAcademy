using System;

namespace StepAcademyApp.Models;

public sealed class Предмет
{
    public uint Id { get; set; }
    public string Название { get; set; }

    public static Предмет FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');
        Предмет Предмет = new Предмет();
        Предмет.Id = (uint)Convert.ToUInt32(values[0]);
        Предмет.Название = values[1];
        return Предмет;
    }
}