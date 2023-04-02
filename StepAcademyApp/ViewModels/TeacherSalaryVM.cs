using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace StepAcademyApp.ViewModels;

internal sealed class TeacherSalaryVM
{
    internal sealed class MockSalary
    {
        public uint Год { get; set; }
        public ushort Месяц { get; set; }
        public decimal Зарплата { get; set; }
    }

    public ObservableCollection<MockSalary> РасчетЗарплаты { get; } = new();
    public TeacherSalaryVM()
    {
        РасчетЗарплаты.Add(new MockSalary
        {
            Год = 2023,
            Месяц = 1,
            Зарплата = 10000
        });
        РасчетЗарплаты.Add(new MockSalary
        {
            Год = 2023,
            Месяц = 2,
            Зарплата = 20000
            
        });РасчетЗарплаты.Add(new MockSalary
        {
            Год = 2023,
            Месяц = 3,
            Зарплата = 30000
        });
    }
}