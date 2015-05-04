using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Infra.Data.Interfaces;

namespace SuperHeroi.Infra.Data.Context
{
    public class BaseDbContext : DbContext, IDbContext
    {
        public BaseDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}
