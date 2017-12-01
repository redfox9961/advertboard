namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Базовая сущность.
    /// </summary>
    /// <typeparam name="TKeyType">Тип первичного ключа.</typeparam>
    public class EntityBase<TKeyType>
    {
        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public TKeyType Id { get; set; }
    }
}
