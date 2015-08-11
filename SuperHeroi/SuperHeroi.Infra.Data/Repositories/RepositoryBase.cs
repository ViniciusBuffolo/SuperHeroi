using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using SuperHeroi.Domain.Interfaces.Repository;
using SuperHeroi.Infra.Data.Context;
using SuperHeroi.Infra.Data.Interfaces;

namespace SuperHeroi.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
        where TEntity : class
        where TContext : IDbContext, new()
    {
        private readonly ContextManager<TContext> _contextManager = ServiceLocator.Current.GetInstance<IContextManager<TContext>>() as ContextManager<TContext>;

        protected IDbSet<TEntity> DbSet;
        protected readonly IDbContext Context;

        public RepositoryBase()
        {
            Context = _contextManager.GetContext();
            DbSet = Context.Set<TEntity>();
        }

        public void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public TEntity GetById(Guid id)
        {
            var entry = DbSet.Find(id);
            Context.Entry(entry).State = EntityState.Detached;
            return entry;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public void Update(TEntity obj)
        {
            var entry = Context.Entry(obj);
            if (entry.State == EntityState.Detached)
                DbSet.Attach(obj);
            entry.State = EntityState.Modified;
        }

        public void Remove(TEntity obj)
        {
            var entry = Context.Entry(obj);
            if (entry.State == EntityState.Detached)
                DbSet.Attach(obj);
            DbSet.Remove(obj);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
