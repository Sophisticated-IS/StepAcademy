namespace StepAcademyApp.Models;

internal sealed class Студент : Гражданин
{
    /// <summary>
    /// null - студент без группы
    /// </summary>
    public uint? IdГруппы { get; set; }
    public ГруппаСтудентов Группа { get; set; }
    
}