using DataAccess.Abstraction;
using System;
using System.Data.Entity;
using System.Data;
using log4net;

namespace DataAccess.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private readonly DbContext _dbContext;

        private DbContextTransaction _dbContextTransaction;

        private readonly ILog _logger;

        #endregion

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = LogManager.GetLogger(typeof(UnitOfWork));

            //WindsorWrapper.Container.Register(Classes.FromAssembly(typeof(UnitOfWork).Assembly)
            //    .BasedOn<IRepositoryBase>()
            //    .WithService.FromInterface()
            //    .LifestyleTransient());

        }

        #region IUnitOfWork
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _dbContextTransaction = _dbContext.Database.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction()
        {
            if(_dbContextTransaction != null)
            {
                _dbContextTransaction.Commit();
                _dbContextTransaction.Dispose();
            }
        }

        public void Dispose()
        {
            if(_dbContextTransaction?.UnderlyingTransaction != null)
            {
                _dbContextTransaction.Rollback();
            }
            _dbContextTransaction.Dispose();
            _dbContext.Dispose();
        }

        public T Repository<T>()
        {
           // return WindsorWrapper.Container.Resolve<T>();
        }

        public void RollbackTransaction()
        {
            if(_dbContextTransaction != null)
            {
                _dbContextTransaction.Rollback();
                _dbContextTransaction.Dispose();
            }
        }

        public void Save()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Error($"Ошибка при сохранении данных в БД: {ex.Message}", ex);
                throw;
            }
        }
        #endregion
    }
}
