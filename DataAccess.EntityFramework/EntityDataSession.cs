using System.Data.Entity;
using DataAccess.Abstraction;

namespace DataAccess.EntityFramework
{
    /// <summary>
    /// Реалиация сессии для работы с БД с использованием EntityFramework.
    /// </summary>
    public class EntityDataSession : IDataSession
    {
        private readonly DbContext _dbContext;

        public EntityDataSession(DbContext dbContext)
        {
            _dbContext = dbContext;

            BeginTransaction();
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CurrentTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                _dbContext.Database.CurrentTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            if (_dbContext.Database.CurrentTransaction != null)
            {
                CommitTransaction();
            }

            _dbContext.Dispose();
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
