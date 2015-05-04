using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Infra.Data.Interfaces;

namespace SuperHeroi.Application.Interfaces
{
    public interface IAppServiceBase<TContext> where TContext : IDbContext
    {
        void BeginTransaction();
        void Commit();
    }
}
