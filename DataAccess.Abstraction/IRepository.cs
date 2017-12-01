using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Abstraction
{
    /// <summary>
    /// Интерфейс базового репозитория.
    /// </summary>
    /// <typeparam name="T">Объект сущности для которой создаётся репозиторий.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Получает дерево выражений для сущности типа <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Дерево выражений сущности.</returns>
        IQueryable<T> All();

        /// <summary>
        /// Возвращает отфильтрованное дерево выражений для получения сущностей, согласно передаваемому предикату.
        /// </summary>
        /// <param name="expression">Выражение для фильтрации.</param>
        /// <returns>Отфильтрованное дерево выражений.</returns>
        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Получает сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Экземпляр сущности.</returns>
        T GetById(int id);

        /// <summary>
        /// Добавляет сущность.
        /// </summary>
        /// <param name="entity">Экземпляр сущности для добавления</param>
        T Add(T entity);

        /// <summary>
        /// Обновляет объект сущности.
        /// </summary>
        /// <param name="entity">Объект сущности для обновления.</param>
        /// <returns>Обновлённый объект сущности.</returns>
        T Update(T entity);

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        /// <param name="entity">Объект сущности для удаления.</param>
        void Delete(T entity);
    }
}
