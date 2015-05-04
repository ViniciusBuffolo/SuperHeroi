using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Application.Interfaces;
using SuperHeroi.Infra.Data.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace SuperHeroi.Application
{
    public class AppServiceBase<TContext> : IAppServiceBase<TContext> where TContext : IDbContext, new()
    {
        private IUnitOfWork<TContext> _uow;

        public virtual void BeginTransaction()
        {
            _uow = ServiceLocator.Current.GetInstance<IUnitOfWork<TContext>>();
            _uow.BeginTransaction();
        }

        public virtual void Commit()
        {
            _uow.SaveChanges();
        }
    }
}
