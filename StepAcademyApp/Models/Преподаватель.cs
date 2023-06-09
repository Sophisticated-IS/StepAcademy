using System;

namespace StepAcademyApp.Models;

internal sealed class Преподаватель : Гражданин
{
    public string НомерТелефона  { get; set; }
    public bool Админ { get; set; }
    public TimeSpan Стаж { get; set; }
}