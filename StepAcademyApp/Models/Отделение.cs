using System;
using System.Security.AccessControl;

namespace StepAcademyApp.Models;

internal sealed class Отделение
{
    public uint Id { get; set; }
    public string Название { get; set; }

    public static Отделение FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');
        Отделение Отделение = new Отделение();
        Отделение.Id = (uint)Convert.ToUInt32(values[0]);
        Отделение.Название = values[1];
        return Отделение;
    }
}