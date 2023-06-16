using System.ComponentModel.DataAnnotations;

namespace StepAcademyApp.Models.LocalizedAttributes;

internal sealed class RegularExpressionPassword : RegularExpressionAttribute
{
    public RegularExpressionPassword(string pattern) : base(pattern)
    {
        
    }

    public override string FormatErrorMessage(string name)
    {
        return "Пароль должен быть от 10 до 32 символов. Содержать минимум 1 цифру, 1 заглавную букву, 1 строчную букву.";
    }
}