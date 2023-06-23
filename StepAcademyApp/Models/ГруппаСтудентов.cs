namespace StepAcademyApp.Models;

public sealed class ГруппаСтудентов
{
    public uint Id { get; set; }
    public  uint СпециальностьId { get; set; }
    public Специальность Специальность { get; set; }
    public  uint ОтделениеId { get; set; }
    public Отделение Отделение { get; set; }
}