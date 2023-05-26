using System;

namespace StepAcademyApp.Models;

internal abstract class Гражданин
{
    public uint  Id { get; set; }
    public uint СерияНомерПаспорта { get; set; }

    public string Имя { get; set; }
    public string Фамилия { get; set; }
    public string Отчество { get; set; }
    
    public DateTime ДатаРождения { get; set; }
}