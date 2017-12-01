using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;
using System.Data.Entity.Migrations;

namespace DataAccess.EntityFramework.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AdvertDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AdvertDbContext context)
        {
            #region ������� ���� ������, ����� __MigrationHistory

            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'IF OBJECT_ID(''?'') NOT IN (ISNULL(OBJECT_ID(''[dbo].[__MigrationHistory]''),0)) DELETE FROM ?'");
            context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");

            #endregion ������� ������ �� ���� ������, ����� __MigrationHistory

            #region ������

            var sevastopol = new City { Name = "�����������" };
            var simferopol = new City { Name = "�����������" };
            var kerch = new City { Name = "�����" };
            var yalta = new City { Name = "����" };
            var moscow = new City { Name = "������" };

            context.Cities.AddRange(new List<City> { sevastopol, simferopol, kerch, yalta, moscow });
            context.SaveChanges();

            #endregion

            #region ������������
            
            context.SaveChanges();

            #endregion

            #region ����������

            var testAdvert1 = new Advert
            {
                Title = "������ �����",
                Text = "�������� �����, ������, 5�� �� ����",
                Cities = new List<City> { moscow },
                Price = 300000,
                PublishDate = DateTime.Now.AddDays(-5),
                ViewsCount = 10000,
            };

            var testAdvert2 = new Advert
            {
                Title = "������ �����",
                Text = "�������� �����, ��������, 20�� �� ����",
                Cities = new List<City> { moscow },
                Price = 50000,
                PublishDate = DateTime.Now,
                ViewsCount = 10000,
            };

            var testAdvert3 = new Advert
            {
                Title = "�������������� ������",
                Text = "�������������� ������ 4.2�. ��������, ������� ��������",
                Cities = new List<City> { sevastopol, simferopol, kerch, yalta },
                PublishDate = DateTime.Now,
                ViewsCount = 1000
            };

            var testAdvert4 = new Advert
            {
                Title = "������ �������� �������",
                Text = "������ �������� ������� ��� ��������, �� ����� �/�, �������� � �� 44 � 48 ������.",
                Cities = new List<City> { sevastopol, yalta },
                PublishDate = DateTime.Now.AddDays(-20),
                Price = 8000,
                ViewsCount = 650
            };

            var testAdvert5 = new Advert
            {
                Title = "Mitsubishi Outlander, 2016",
                Text = "����� ������. �� ���� �� ������ .������ ����� ��� .������ �������.����",
                PublishDate = DateTime.Now.AddDays(-1),
                Price = 1250000,
                ViewsCount = 105
            };

            var testAdvert6 = new Advert
            {
                Title = "������� ����� � �����. �����",
                Text = "��������� ������� ������� �� ������ � ��������� �������. ����� � ��� �� ���.",
                Cities = new List<City> { sevastopol },
                PublishDate = DateTime.Now.AddDays(-7),
                ViewsCount = 25
            };

            var testAdvert7 = new Advert
            {
                Title = "������",
                Text = "������ �� ����, �����, ���������� ��� ����. ������ 28. ��������� ��������, ������ 3 ������ �����.",
                Cities = new List<City> { simferopol },
                PublishDate = DateTime.Now.AddDays(-2),
                Price = 1600,
                ViewsCount = 250
            };

            context.Adverts.AddRange(new List<Advert> { testAdvert1, testAdvert2, testAdvert3, testAdvert4, testAdvert5, testAdvert6, testAdvert7 });
            context.SaveChanges();

            #endregion
        }
    }
}
