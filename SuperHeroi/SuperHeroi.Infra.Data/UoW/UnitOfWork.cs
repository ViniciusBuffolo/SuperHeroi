using System;
using SuperHeroi.Infra.Data.Context;
using SuperHeroi.Infra.Data.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace SuperHeroi.Infra.Data.UoW
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : IDbContext, new()
    {
        private readonly IDbContext _dbContext;
        private readonly ContextManager<TContext> _contextManager = ServiceLocator.Current.GetInstance<IContextManager<TContext>>() as ContextManager<TContext>;

        private bool _disposed;

        public UnitOfWork()
        {
            _dbContext = _contextManager.GetContext();
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
