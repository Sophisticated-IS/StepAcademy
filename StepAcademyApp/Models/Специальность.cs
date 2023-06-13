using System;

namespace StepAcademyApp.Models;

internal sealed class Специальность
{
    public uint Id { get; set; }
    public string Название { get; set; }

    public static Специальность FromCsv(string csvLine)
    {
        string[] values = csvLine.Split(',');
        Специальность Специальность = new Специальность();
        Специальность.Id = (uint)Convert.ToUInt32(values[0]);
        Специальность.Название = values[1];
        return Специальность;
    }
}