using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BusinessLogic.Model.Dtos;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappings;
using BusinessLogicLayer.Services;
using DataAccess.Abstraction;
using DataAccessLayer.Entities;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace AdvertBoard.Tests
{
    [TestFixture]
    public class AdvertServiceTests
    {
        private IAdvertService _advertService;
        private Mock<IRepository<Advert>> _advertRepositoryMock;

        private List<City> _testCities;
        private List<ApplicationUser> _testUsers;
        private List<Advert> _testAdverts;
        private List<AdvertDto> _testAdvertDtos;

        [SetUp]
        public void Setup()
        {
            MapperConfigurator.Configure();

            #region Города

            var sevastopol = new City { Id = 1, Name = "Севастополь" };
            var simferopol = new City { Id = 2, Name = "Симферополь" };
            var kerch = new City { Id = 3, Name = "Керчь" };
            var yalta = new City { Id = 4, Name = "Ялта" };
            var moscow = new City { Id = 5, Name = "Москва" };

            #endregion

            #region Пользователи

            //var user1 = new User
            //{
            //    Id = 1,
            //    UserName = "user1",
            //    Password = "123456"
            //};

            //var user2 = new User
            //{
            //    Id = 2,
            //    UserName = "user2",
            //    Password = "123456"
            //};

            //var user3 = new User
            //{
            //    Id = 3,
            //    UserName = "user3",
            //    Password = "123456"
            //};

            #endregion

            #region Объявления

            var testAdvert1 = new Advert
            {
                Id = 1,
                Title = "Продам гараж",
                Text = "Продаётся гараж, дорого, 5км от МКАД",
                Cities = new List<City> { moscow },
                Price = 300000,
                PublishDate = DateTime.UtcNow.AddDays(-5),
                ViewsCount = 10000,
            };

            var testAdvert2 = new Advert
            {
                Id = 2,
                Title = "Продам гараж",
                Text = "Продаётся гараж, недорого, 20км от МКАД",
                Cities = new List<City> { moscow },
                Price = 50000,
                PublishDate = DateTime.UtcNow,
                ViewsCount = 10000,
            };

            var testAdvert3 = new Advert
            {
                Id = 3,
                Title = "Грузоперевозки Газель",
                Text = "Грузоперевозки Газель 4.2м. Переезды, трезвые грузчики",
                Cities = new List<City> { sevastopol, simferopol, kerch, yalta },
                PublishDate = DateTime.UtcNow,
                ViewsCount = 1000
            };

            var testAdvert4 = new Advert
            {
                Id = 4,
                Title = "Куртка норковая вязаная",
                Text = "Куртка норковая вязания без подклада, на замке б/у, подойдет и на 44 и 48 размер.",
                Cities = new List<City> { sevastopol, yalta },
                PublishDate = DateTime.UtcNow.AddDays(-20),
                Price = 8000,
                ViewsCount = 650
            };

            var testAdvert5 = new Advert
            {
                Id = 5,
                Title = "Mitsubishi Outlander, 2016",
                Text = "Новая машина. Не бита не сколов .машина стоит ждёт .нового хозяина.торг",
                PublishDate = DateTime.UtcNow.AddDays(-1),
                Price = 1250000,
                ViewsCount = 105
            };

            var testAdvert6 = new Advert
            {
                Id = 6,
                Title = "Стрижка собак и кошек. Выезд",
                Text = "Стоимость стрижки зависит от породы и характера питомца. Выезд к Вам на дом.",
                Cities = new List<City> { sevastopol },
                PublishDate = DateTime.UtcNow.AddDays(-7),
                ViewsCount = 25
            };

            var testAdvert7 = new Advert
            {
                Id = 7,
                Title = "Сапоги",
                Text = "Сапоги на меху, тёплые, комфортные для ноги. Размер 28. Состояние отличное, носили 3 месяца всего.",
                Cities = new List<City> { simferopol },
                PublishDate = DateTime.Now.AddDays(-2),
                Price = 1600,
                ViewsCount = 250
            };

            #endregion

            _testAdverts = new List<Advert> { testAdvert1, testAdvert2, testAdvert3, testAdvert4, testAdvert5, testAdvert6, testAdvert7 };
            _testAdvertDtos = Mapper.Map<IEnumerable<Advert>, List<AdvertDto>>(_testAdverts);
            //_testUsers = new List<User> {user1, user2, user3};
            _testCities = new List<City> {sevastopol, simferopol, moscow, yalta, kerch};
            _advertRepositoryMock = new Mock<IRepository<Advert>>();
            _advertService = new AdvertService(_advertRepositoryMock.Object);
        }

        [Test]
        public void GetById_ReturnsAdvert()
        {
            //Arrange
            var jacketAdvert = _testAdverts.First(adv => adv.Title == "Куртка норковая вязаная");
            var jackerAdvertId = jacketAdvert.Id;

            _advertRepositoryMock.Setup(repo => repo.GetById(jackerAdvertId)).Returns(jacketAdvert);

            //Act
            var returnedAdvert = _advertService.GetById(jackerAdvertId);

            //Assert
            Assert.AreEqual(returnedAdvert.Title, "Куртка норковая вязаная");
        }

        [Test]
        public void DeleteAdvert_DeletesAdvert()
        {
            //Arrange
            var carAdvert = _testAdverts.First(adv => adv.Title == "Mitsubishi Outlander, 2016");
            var carAdvertId = carAdvert.Id;

            _advertRepositoryMock.Setup(repo => repo.GetById(carAdvertId))
                .Returns(carAdvert);

            //Act
            _advertService.DeleteAdvert(carAdvert.Id);
            
            //Assert
            _advertRepositoryMock.Verify(repo => repo.Delete(carAdvert), Times.Once);
        }

        [Test]
        public void GetByCity_ReturnsAdvertsByCity()
        {
            //Arrange
            var moscowCity = _testCities.First(c => c.Name == "Москва");

            _advertRepositoryMock.Setup(repo => repo.Where(It.IsAny<Expression<Func<Advert, bool>>>()))
                .Returns((Expression<Func<Advert, bool>> expr) => _testAdverts.AsQueryable().Where(expr));  

            //Act
            var advertsForMoscow = _advertService.GetByCity(moscowCity.Id);

            //Assert 
            Assert.AreEqual(advertsForMoscow.Count, 2);
            Assert.IsTrue(advertsForMoscow.All(adv => adv.Title == "Продам гараж"));
            Assert.AreNotEqual(advertsForMoscow[0].Price, advertsForMoscow[1].Price);
        }

        [Test]
        public void SaveAdvert_AddsNewAdvert()
        {
            //Arrange
            var newAdvertDto = _testAdvertDtos.First(a => a.Title == "Грузоперевозки Газель");
            newAdvertDto.Id = 0;

            var savedAdvert = new Advert();

            _advertRepositoryMock.Setup(repo => repo.Add(It.IsAny<Advert>()))
                .Callback<Advert>(adv => savedAdvert = adv);

            //Act
            _advertService.SaveAdvert(newAdvertDto);

            //Assert
            Assert.AreEqual("Грузоперевозки Газель", savedAdvert.Title);
        }

        [Test]
        public void SaveAdvert_ThrowsArgumentNull_IfUserIsNull()
        {
            //Arrange
            AdvertDto advert = null;

            //Act
            try
            {
                _advertService.SaveAdvert(advert);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsInstanceOf<ArgumentNullException>(ex);
            }
        }

        [Test]
        public void GetMostPopular_ReturnsMostPopularAdverts()
        {
            //Arrange
            _advertRepositoryMock.Setup(repo => repo.All()).Returns(_testAdverts.AsQueryable());

            //Act
            var topAdverts = _advertService.GetMostPopular(2);

            //Assert
            Assert.AreEqual(2, topAdverts.Count);
            Assert.AreEqual(2, topAdverts[0].Id);
            Assert.AreEqual(3, topAdverts[1].Id);
        }
    }
}
