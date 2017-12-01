using System;
using System.Data.Entity;

namespace DataAccess.Abstraction
{
    /// <summary>
    /// Интерфейс сессии для работы с персистентным хранилищем данных.
    /// </summary>
    public interface IDataSession : IDisposable
    {
        /// <summary>
        /// Возвращает множество данных типа <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип хранимых данных.</typeparam>
        IDbSet<T> Set<T>() where T : class;

        /// <summary>
        /// Сохраняет изменения, внесённые в течение взаимодействия с сессией.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Начинает транзакцию в текущей сессии.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Выполняет транзакцию.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Отменяет выполнение транзакции.
        /// </summary>
        void RollbackTransaction();
    }
}
