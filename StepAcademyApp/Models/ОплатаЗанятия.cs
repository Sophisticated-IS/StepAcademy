using Microsoft.CodeAnalysis.Scripting.Hosting;

namespace StepAcademyApp.Models;

internal sealed class ОплатаЗанятия
{
    public uint Id { get; set; }
    public uint IdТипЗанятия { get; set; }
    public ТипЗанятия ТипЗанятия { get; set; }
    public uint IdПредмет { get; set; }
    public Предмет Предмет { get; set; }
    public decimal ЧасоваяОплата { get; set; }
}