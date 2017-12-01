using System;
using System.Collections.Generic;

namespace BusinessLogic.Model.Dtos
{
    /// <summary>
    /// Транспортный объект для сущности объявления.
    /// </summary>
    public class AdvertDto : DtoBase<int>
    {
        /// <summary>
        /// Идентификатор пользователя, разместившего объявление.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Заглавие объявления.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст объявления.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Стоимость товара/услуги.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Дата публикации объявления.
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Количество просмотров.
        /// </summary>
        public int ViewsCount { get; set; }

        /// <summary>
        /// Города, к которым относится объявление.
        /// </summary>
        public IEnumerable<CityDto> Cities { get; set; }
    }
}
