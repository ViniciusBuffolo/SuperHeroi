using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroi.Infra.Data.Interfaces
{
    public interface IContextManager<TContext> where TContext : IDbContext, new()
    {
        IDbContext GetContext();
    }
}
