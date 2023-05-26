namespace StepAcademyApp.Models;

internal sealed class Нагрузка
{
    public uint Id { get; set; }
    public uint IdПреподавателя { get; set; }
    public Преподаватель Преподаватель { get; set; }
    public uint IdГруппы { get; set; }
    public ГруппаСтудентов Группа { get; set; }
    public uint КолвоЧасов { get; set; }
    public uint IdПредмета { get; set; }
    public Предмет Предмет { get; set; }
    public uint IdТипЗанятия { get; set; }
    public ТипЗанятия ТипЗанятия { get; set; }
}