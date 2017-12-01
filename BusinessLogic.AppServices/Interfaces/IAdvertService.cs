using System.Collections.Generic;
using BusinessLogic.Model.Dtos;

namespace BusinessLogicLayer.Interfaces
{
    /// <summary>
    /// Сервис для работы с объявлениями.
    /// </summary>
    public interface IAdvertService
    {
        /// <summary>
        /// Получает объявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        /// <returns>Объявление.</returns>
        AdvertDto GetById(int id);

        /// <summary>
        /// Сохраняет объявление.
        /// </summary>
        /// <param name="advert"></param>
        void SaveAdvert(AdvertDto advert);

        /// <summary>
        /// Удаляет объявление по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор объявления.</param>
        void DeleteAdvert(int id);

        /// <summary>
        /// Возвращает все объявления указанного пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Спосок объявлений.</returns>
        List<AdvertDto> GetByUser(int userId);

        /// <summary>
        /// Возвращает все объявления для указанного города.
        /// </summary>
        /// <param name="cityId">Идентификатор города.</param>
        /// <returns>Список объявлений.</returns>
        List<AdvertDto> GetByCity(int cityId);

        /// <summary>
        /// Возвращает наиболее популярные объявления.
        /// </summary>
        /// <param name="count">Количество объявлений</param>
        /// <returns>Спосок объявлений.</returns>
        List<AdvertDto> GetMostPopular(int count);

        /// <summary>
        /// Возвращает все объявления.
        /// </summary>
        /// <returns>Спосок объявлений.</returns>
        List<AdvertDto> GetAllAdverts();
    }
}
