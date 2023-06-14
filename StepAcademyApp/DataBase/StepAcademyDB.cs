using Microsoft.EntityFrameworkCore;
using StepAcademyApp.Models;

namespace StepAcademyApp.DataBase;

internal sealed class StepAcademyDB : DbContext
{
    public DbSet<Гражданин> Граждани { get; set; }
    public DbSet<Преподаватель> Учителя { get; set; }
    public DbSet<Студент> Студенты { get; set; }
    public DbSet<ГруппаСтудентов> ГруппыСтудентов { get; set; }
    public DbSet<Зарплата> Зарплаты { get; set; }
    public DbSet<Нагрузка> Нагрузка { get; set; }
    public DbSet<ОплатаЗанятия> ОплатаЗанятий { get; set; }
    public DbSet<Отделение> Отделения { get; set; }
    public DbSet<Оценка> Оценки { get; set; }
    public DbSet<Предмет> Предметы { get; set; }
    public DbSet<Специальность> Специальности { get; set; }
    public DbSet<ТипЗанятия> ТипЗанятий { get; set; }
    public DbSet<УчетныеДанные> УчетныеДанные { get; set; }
    public DbSet<Аудит> Аудит { get; set; }
    

    public StepAcademyDB(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Гражданин>().UseTpcMappingStrategy();
        modelBuilder.Entity<УчетныеДанные>()
                    .HasKey(p=>p.Логин);
        
    }
}