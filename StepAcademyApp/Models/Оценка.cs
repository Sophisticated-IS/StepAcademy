namespace StepAcademyApp.Models;

internal sealed class Оценка
{
    public uint Idстудента { get; set; }
    public uint IdГруппы { get; set; }
    public uint IdПредмета { get; set; }
    public short Балл { get; set; }
}