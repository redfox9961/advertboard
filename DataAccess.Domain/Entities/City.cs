using System.Collections;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Город.
    /// </summary>
    public class City : EntityBase<int>
    {
        /// <summary>
        /// Наименование города.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Объявления, связанные с городом.
        /// </summary>
        public virtual ICollection<Advert> Adverts { get; set; }
    }
}
