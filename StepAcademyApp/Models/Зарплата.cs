
using System;
using System.Xml.Linq;

namespace StepAcademyApp.Models;

internal sealed class Зарплата
{
    public uint Id { get; set; }
    public uint Year { get; set; }
    public uint Month { get; set; }
    public uint IdУчитель { get; set; }
    public Преподаватель Преподаватель { get; set; }
    public decimal СуммаВыплат { get; set; }

    public static Зарплата FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');
        Зарплата Зарплата = new Зарплата();
        Зарплата.Id = (uint)Convert.ToUInt32(values[0]);
        Зарплата.Year = (uint)Convert.ToUInt32(values[1]);
        Зарплата.Month = (uint)Convert.ToUInt32(values[2]);
        Зарплата.IdУчитель = (uint)Convert.ToUInt32(values[3]);
        Зарплата.СуммаВыплат = Convert.ToInt32(values[4]);
        
        return Зарплата;
    }
}