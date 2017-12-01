using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Объявление.
    /// </summary>
    public class Advert : EntityBase<int>
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
        /// Пользователь, разместивший объявление.
        /// </summary>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Города, к которым относится объявление.
        /// </summary>
        public virtual ICollection<City> Cities { get; set; }
    }
}
