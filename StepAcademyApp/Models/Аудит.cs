using System;

namespace StepAcademyApp.Models;

internal sealed class Аудит
{
    public uint Id { get; set; }
    public string Message { get; set; }
    public DateTime Time {get; set; }
}