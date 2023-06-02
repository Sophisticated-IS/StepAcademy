using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using StepAcademyApp.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<Models.���������> listOtdel = new List<Models.���������>();
            List<Models.�������������> listSpecial = new List<Models.�������������>();
            List<Models.�������> listPredmet = new List<Models.�������>();
            List<Models.����������> listTipZan = new List<Models.����������>();
            for (int i = 0; i < 10; i++)
            {
                listOtdel.Add(new Models.���������
                {
                    Id = (uint)(i + 1),
                    Name = "�������� ��������� " + i
                });
                listSpecial.Add(new Models.�������������
                {
                    Id = (uint)(i + 1),
                    Name = "�������� ������������� " + i
                });
                listPredmet.Add(new Models.�������
                {
                    Id = (uint)(i + 1),
                    Name = "�������� �������� " + i
                });
                listTipZan.Add(new Models.����������
                {
                    Id = (uint)(i + 1),
                    Name = "�������� ������� " + i
                });
            }
            dbContext.���������.AddRange(
                    listOtdel
                    );
            dbContext.�������������.AddRange(
                    listSpecial
                    );
            dbContext.��������.AddRange(
                    listPredmet
                    );
            dbContext.����������.AddRange(
                    listTipZan
                    );
            dbContext.SaveChanges();
            List<Models.�������������> listPrepod = new List<Models.�������������>();
            List<Models.���������������> listGrup = new List<Models.���������������>();
            List<Models.�������������> listOplataZan = new List<Models.�������������>();
            List<Models.��������> listZarplat = new List<Models.��������>();
            List<Models.��������> listNagruzka = new List<Models.��������>();
            for (int i = 0; i < 100; i++)
            {
                listOplataZan.Add(
                    new Models.�������������
                    {
                        Id = (uint)(i + 1),
                        Id������� = listPredmet[i % 10].Id,
                        ������� = listPredmet[i % 10],
                        Id���������� = listTipZan[i % 10].Id,
                        ���������� = listTipZan[i % 10],
                        ������������� = (decimal)((i + 1) * 100.1),
                    }
                    );
                listPrepod.Add(
                    new Models.�������������
                    {
                        Id = (uint)(i + 1),
                        ������������������ = (uint)(i + 1),
                        ��� = "��� " + (i + 1),
                        ������� = "������� " + (i + 1),
                        �������� = "�������� " + (i + 1),
                        ������������ = new DateTime(System.DateTime.Now.Ticks + i, DateTimeKind.Utc),
                        ������������� = $"89{i}",
                        ���� = (TimeSpan)(DateTime.SpecifyKind(new DateTime(2010 + i, 1, 1, 8, 0, 0), DateTimeKind.Utc) - DateTime.SpecifyKind(new DateTime(2010, 1, 1, 8, 0, 0), DateTimeKind.Utc))
                    }
                    );
                listZarplat.Add(
                    new Models.��������
                    {
                        Id = (uint)(i + 1),
                        Id������� = listPrepod[i].Id,
                        ������������� = listPrepod[i],
                        Year = (uint)(i + 1),
                        Month = (uint)(i + 1),
                        ����������� = (decimal)((i + 1) * 100.1),
                    }
                    );
                listGrup.Add(
                    new Models.���������������
                    {
                        Id = (uint)(i + 1),
                        ���������Id = listOtdel[i % 10].Id,
                        ��������� = listOtdel[i % 10],
                        �������������Id = listSpecial[i % 10].Id,
                        ������������� = listSpecial[i % 10],
                    }
                    );
                listNagruzka.Add(
                    new Models.��������
                    {
                        Id = (uint)(i + 1),
                        Id������ = listGrup[i].Id,
                        ������ = listGrup[i],
                        Id�������� = listPredmet[i % 10].Id,
                        ������� = listPredmet[i % 10],
                        Id������������� = listPrepod[i].Id,
                        ������������� = listPrepod[i],
                        Id���������� = listTipZan[i % 10].Id,
                        ���������� = listTipZan[i % 10],
                        ���������� = (uint)(i + 100),
                    }
                    );
            }
            dbContext.�������������.AddRange(
                listOplataZan
                    );
            dbContext.�������.AddRange(
                listPrepod
                    );
            dbContext.��������.AddRange(
                listZarplat
                    );
            dbContext.���������������.AddRange(
                listGrup
                );
            dbContext.��������.AddRange(
                    listNagruzka
                    );
            dbContext.SaveChanges();
            var studId = 101u;
            List<Models.�������> listStudents = new List<Models.�������>();
            List<Models.������> listOcen = new List<Models.������>();
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 1000; j++,studId++)
                {
                    listStudents.Add(
                        new Models.�������
                        {
                            Id = studId,
                            ������������������ = (uint)(((1000 * i) + j + 1) + 100000),
                            ��� = "��� " + (1000 * i) + j + 1,
                            ������� = "������� " + (1000 * i) + j + 1,
                            �������� = "�������� " + (1000 * i) + j + 1,
                            ������������ = new DateTime(System.DateTime.Now.Ticks + i, DateTimeKind.Utc),
                            Id������ = listGrup[i].Id,
                            ������ = listGrup[i]
                        }
                        );
                    listOcen.Add(
                        new Models.������
                        {
                            Id = (uint)((1000 * i) + j + 1),
                            Id�������� = listStudents[(1000 * i) + j].Id,
                            ������� = listStudents[(1000 * i) + j],
                            Id������ = listStudents[(1000 * i) + j].Id������.HasValue ? listStudents[(1000 * i) + j].Id������.Value : default,
                            ������ = listStudents[(1000 * i) + j].������,
                            Id�������� = listPredmet[j % 10].Id,
                            ������� = listPredmet[j % 10],
                            ���� = (short)(j + 1),
                        }
                        );
                }
                                              
            }
            dbContext.��������.AddRange(
                    listStudents
                        );
            dbContext.������.AddRange(
                    listOcen
                        );
            dbContext.SaveChanges();
        }
    }
}