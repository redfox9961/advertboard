using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic.Model.Dtos;
using BusinessLogicLayer.Interfaces;
using DataAccess.Abstraction;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Services
{
    public class AdvertService: IAdvertService
    {
        private readonly IRepository<Advert> _advertRepository;

        public AdvertService(IRepository<Advert> advertRepository)
        {
            _advertRepository = advertRepository;
        }

        public AdvertDto GetById(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Не указан идентификатор пользователя.");
            }

            var advert = _advertRepository.GetById(id);

            if (advert == null)
            {
                throw new ApplicationException("Объявление не найдено.");
            }

            return Mapper.Map<Advert, AdvertDto>(advert);
        }

        public void SaveAdvert(AdvertDto advert)
        {
            if (advert == null)
            {
                throw new ArgumentNullException(nameof(advert));
            }

            var advertDal = Mapper.Map<AdvertDto, Advert>(advert);

            if (advertDal.Id == 0)
            {
                advertDal.PublishDate = DateTime.UtcNow;
                _advertRepository.Add(advertDal);
            }
            else
            {
                _advertRepository.Update(advertDal);
            }
        }

        public void DeleteAdvert(int id)
        {
            var entityToDelete = _advertRepository.GetById(id);

            if (entityToDelete == null)
            {
                throw new ApplicationException("Объявление не найдено.");
            }

            _advertRepository.Delete(entityToDelete);
        }

        public List<AdvertDto> GetByUser(int userId)
        {
            if (userId == 0)
            {
                throw new ArgumentException("Не указан идентификатор пользователя.");
            }

            var userAdverts = _advertRepository
                .Where(a => a.UserId.HasValue && a.UserId == userId)
                .ToList();

            return Mapper.Map<IEnumerable<Advert>, List<AdvertDto>>(userAdverts);
        }

        public List<AdvertDto> GetByCity(int cityId)
        {
            if (cityId == 0)
            {
                throw new ArgumentException("Не указан идентификатор города.");
            }

            var userAdverts = _advertRepository
                .Where(a => a.Cities != null && a.Cities.Any(c => c.Id == cityId))
                .ToList();

            return Mapper.Map<IEnumerable<Advert>, List<AdvertDto>>(userAdverts);
        }

        public List<AdvertDto> GetMostPopular(int count)
        {
            var allAdverts = _advertRepository.All().ToList();

            var mostPopular = allAdverts.OrderByDescending(a => a.ViewsCount / ((DateTime.UtcNow - a.PublishDate).TotalSeconds + 1))
                .Take(count)
                .ToList();

            return Mapper.Map<IEnumerable<Advert>, List<AdvertDto>>(mostPopular);
        }

        public List<AdvertDto> GetAllAdverts()
        {
            var adverts = _advertRepository.All().ToList();

            return Mapper.Map<IEnumerable<Advert>, List<AdvertDto>>(adverts);
        }
    }
}
