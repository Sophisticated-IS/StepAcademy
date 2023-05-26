namespace StepAcademyApp.Models;

internal sealed class Оценка
{
    public uint Id { get; set; }
    public uint IdСтудента { get; set; }
    public Студент Студент { get; set; }
    public uint IdГруппы { get; set; }
    public ГруппаСтудентов Группа { get; set; }
    public uint IdПредмета { get; set; }
    public Предмет Предмет { get; set; }
    public short Балл { get; set; }
}