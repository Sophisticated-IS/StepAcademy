using System;

namespace StepAcademyApp.Models;

public sealed class ТипЗанятия
{
    public uint Id { get; set; }
    public string Название { get; set; }

    public static ТипЗанятия FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');
        ТипЗанятия ТипЗанятия = new ТипЗанятия();
        ТипЗанятия.Id = (uint)Convert.ToUInt32(values[0]);
        ТипЗанятия.Название = values[1];
        return ТипЗанятия;
    }
}