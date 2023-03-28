namespace StepAcademyApp.Models;

internal sealed class Нагрузка
{
    public uint IdПреподавателя { get; set; }
    public uint IdГруппы { get; set; }
    public uint КолвоЧасов { get; set; }
    public uint IdПредмета { get; set; }
    public uint IdТипЗанятия { get; set; }
}