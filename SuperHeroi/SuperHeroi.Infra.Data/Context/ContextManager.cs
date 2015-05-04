using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SuperHeroi.Infra.Data.Interfaces;

namespace SuperHeroi.Infra.Data.Context
{
    public class ContextManager<TContext> : IContextManager<TContext> where TContext : IDbContext, new()
    {
        private const string ContextKey = "ContextManager.Context";
        public IDbContext GetContext()
        {
            if (HttpContext.Current.Items[ContextKey] == null)
            {
                HttpContext.Current.Items[ContextKey] = new TContext();
            }

            return (IDbContext)HttpContext.Current.Items[ContextKey];
        }

    }
}
