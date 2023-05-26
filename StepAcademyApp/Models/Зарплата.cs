
using System.Xml.Linq;

namespace StepAcademyApp.Models;

internal sealed class Зарплата
{
    public uint Id { get; set; }
    public uint Year { get; set; }
    public uint Month { get; set; }
    public uint IdУчитель { get; set; }
    public Преподаватель Преподаватель { get; set; }
    public decimal СуммаВыплат { get; set; }
}