using System;

namespace StepAcademyApp.Models;

internal sealed class Преподаватель : Гражданин
{
    public string НомерТелефона  { get; set; }
    public TimeSpan Стаж { get; set; }
}