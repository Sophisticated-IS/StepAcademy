using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace StepAcademyApp.Models;

internal sealed class УчетныеДанные
{ 
    public string Логин { get; set; }
    public string Пароль { get; set; }
    public string Соль { get; set; }
    public Гражданин Гражданин { get; set; }
}