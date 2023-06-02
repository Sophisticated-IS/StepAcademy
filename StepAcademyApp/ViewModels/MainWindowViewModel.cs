using ReactiveUI.Fody.Helpers;

namespace StepAcademyApp.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {

        [Reactive]
        public string RoleName { get; set; } = "Студент";
        [Reactive]
        public string UserName { get; set; } = "Сова Игорь Сергеевич";

        public bool IsStudentRole { get; set; } = true;
        
        
        public MainWindowViewModel()
        {
            
        }
    }
}