using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroi.Domain.Interfaces.Repository
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(Guid id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(TEntity obj);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
