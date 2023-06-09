using ReactiveUI.Fody.Helpers;
using StepAcademyApp.Models;

namespace StepAcademyApp.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {

        [Reactive]
        public string RoleName { get; set; }
        [Reactive]
        public string UserName { get; set; }

        [Reactive]
        public bool IsStudentRole { get; set; }
        
        
        public MainWindowViewModel()
        {
            RoleName = Program.CurrentUser is Студент? "Студент":"Преподаватель";
            IsStudentRole = Program.CurrentUser is Студент;
            UserName = Program.CurrentUser.Имя + " " + Program.CurrentUser.Фамилия + " " + Program.CurrentUser.Отчество;
        }
    }
}