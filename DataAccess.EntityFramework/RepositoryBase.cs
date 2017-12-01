using System;
using DataAccess.Abstraction;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using log4net;

namespace DataAccess.EntityFramework
{
    /// <summary>
    /// Базовый класс репозитория.
    /// </summary>
    /// <typeparam name="T">Тип данных, для работы с которыми используется репозиторий.</typeparam>
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly IDataSession _dataSession;

        private readonly ILog _logger;

        public RepositoryBase(IDataSession dataSession)
        {
            _dataSession = dataSession;
            _logger = LogManager.GetLogger(typeof(RepositoryBase<T>));
        }

        /// <summary>
        /// Получает дерево выражений для сущности типа <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Дерево выражений сущности.</returns>
        public IQueryable<T> All()
        {
            var result = _dataSession.Set<T>().Where(c => true);
            return result;
        }

        /// <summary>
        /// Возвращает отфильтрованное дерево выражений для получения сущностей, согласно передаваемому предикату.
        /// </summary>
        /// <param name="expression">Выражение для фильтрации.</param>
        /// <returns>Отфильтрованное дерево выражений.</returns>
        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            var dataSet = _dataSession.Set<T>();
            IQueryable<T> queryable = dataSet;

  
            var result = queryable.Where(expression);
            return result;
        }

        /// <summary>
        /// Получает сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Экземпляр сущности.</returns>
        public T GetById(int id)
        {
            var result = _dataSession.Set<T>().Find(id);
            return result;
        }

        /// <summary>
        /// Добавляет сущность.
        /// </summary>
        /// <param name="entity">Экземпляр сущности для добавления</param>
        public T Add(T entity)
        {
            T result;
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                result = _dataSession.Set<T>().Add(entity);
                _dataSession.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorMessage = GetErrorMessage(dbEx);
                throw new Exception(errorMessage, dbEx);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                _dataSession.RollbackTransaction();
                throw;
            }
            return result;
        }

        /// <summary>
        /// Обновляет объект сущности.
        /// </summary>
        /// <param name="entity">Объект сущности для обновления.</param>
        /// <returns>Обновлённый объект сущности.</returns>
        public T Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                _dataSession.SaveChanges();
                return entity;
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorMessage = GetErrorMessage(dbEx);
                throw new Exception(errorMessage, dbEx);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                _dataSession.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Удаляет сущность.
        /// </summary>
        /// <param name="entity">Объект сущности для удаления.</param>
        public void Delete(T entity)
        {
            try
            {
                _dataSession.Set<T>().Remove(entity);
                _dataSession.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorMessage = GetErrorMessage(dbEx);
                throw new Exception(errorMessage, dbEx);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                _dataSession.RollbackTransaction();
                throw;
            }
        }

        #region Helpers

        private static string GetErrorMessage(DbEntityValidationException dbEx)
        {
            var errorMessage = dbEx.EntityValidationErrors
                .SelectMany(validationErrors => validationErrors.ValidationErrors)
                .Aggregate(string.Empty, (current, validationError) =>
                    current +
                    ($"Свойство: {validationError.PropertyName} Ошибка: {validationError.ErrorMessage}" + Environment.NewLine));
            return errorMessage;
        }

        #endregion
    }
}