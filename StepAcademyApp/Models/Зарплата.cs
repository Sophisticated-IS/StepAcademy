
namespace StepAcademyApp.Models;

internal sealed class Зарплата
{
    public uint Year { get; set; }
    public uint Month { get; set; }
    public uint IdУчитель { get; set; }
    public decimal СуммаВыплат { get; set; }
}