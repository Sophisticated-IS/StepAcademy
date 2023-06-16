namespace StepAcademyApp.Models.LocalizedAttributes;

public sealed class MinLengthAttributeCustom : System.ComponentModel.DataAnnotations.MinLengthAttribute
{
    public MinLengthAttributeCustom(int length) : base(length)
    {
    }
    
    public override string FormatErrorMessage(string name)
    {
        return $"Минимальная длина составлять {Length} символов";
    }
}