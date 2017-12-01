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
            #region Очистка всех таблиц, кроме __MigrationHistory

            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'IF OBJECT_ID(''?'') NOT IN (ISNULL(OBJECT_ID(''[dbo].[__MigrationHistory]''),0)) DELETE FROM ?'");
            context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");

            #endregion Удаляет данные из всех таблиц, кроме __MigrationHistory

            #region Города

            var sevastopol = new City { Name = "Севастополь" };
            var simferopol = new City { Name = "Симферополь" };
            var kerch = new City { Name = "Керчь" };
            var yalta = new City { Name = "Ялта" };
            var moscow = new City { Name = "Москва" };

            context.Cities.AddRange(new List<City> { sevastopol, simferopol, kerch, yalta, moscow });
            context.SaveChanges();

            #endregion

            #region Пользователи
            
            context.SaveChanges();

            #endregion

            #region Объявления

            var testAdvert1 = new Advert
            {
                Title = "Продам гараж",
                Text = "Продаётся гараж, дорого, 5км от МКАД",
                Cities = new List<City> { moscow },
                Price = 300000,
                PublishDate = DateTime.Now.AddDays(-5),
                ViewsCount = 10000,
            };

            var testAdvert2 = new Advert
            {
                Title = "Продам гараж",
                Text = "Продаётся гараж, недорого, 20км от МКАД",
                Cities = new List<City> { moscow },
                Price = 50000,
                PublishDate = DateTime.Now,
                ViewsCount = 10000,
            };

            var testAdvert3 = new Advert
            {
                Title = "Грузоперевозки Газель",
                Text = "Грузоперевозки Газель 4.2м. Переезды, трезвые грузчики",
                Cities = new List<City> { sevastopol, simferopol, kerch, yalta },
                PublishDate = DateTime.Now,
                ViewsCount = 1000
            };

            var testAdvert4 = new Advert
            {
                Title = "Куртка норковая вязаная",
                Text = "Куртка норковая вязания без подклада, на замке б/у, подойдет и на 44 и 48 размер.",
                Cities = new List<City> { sevastopol, yalta },
                PublishDate = DateTime.Now.AddDays(-20),
                Price = 8000,
                ViewsCount = 650
            };

            var testAdvert5 = new Advert
            {
                Title = "Mitsubishi Outlander, 2016",
                Text = "Новая машина. Не бита не сколов .машина стоит ждёт .нового хозяина.торг",
                PublishDate = DateTime.Now.AddDays(-1),
                Price = 1250000,
                ViewsCount = 105
            };

            var testAdvert6 = new Advert
            {
                Title = "Стрижка собак и кошек. Выезд",
                Text = "Стоимость стрижки зависит от породы и характера питомца. Выезд к Вам на дом.",
                Cities = new List<City> { sevastopol },
                PublishDate = DateTime.Now.AddDays(-7),
                ViewsCount = 25
            };

            var testAdvert7 = new Advert
            {
                Title = "Сапоги",
                Text = "Сапоги на меху, тёплые, комфортные для ноги. Размер 28. Состояние отличное, носили 3 месяца всего.",
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
