using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using StepAcademyApp.DataBase;

namespace StepAcademyApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5436;Database=StepAcademyDB;Username=postgres;Password=postgres");
            var dbContext = new StepAcademyDB(optionsBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }
}