using System;

namespace StepAcademyApp.Models;

internal sealed class Учитель : Гражданин
{
    public string НомерТелефона  { get; set; }
    public TimeSpan Стаж { get; set; }
}