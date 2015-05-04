using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using SuperHeroi.Application;
using SuperHeroi.Application.Interfaces;
using SuperHeroi.Domain.Interfaces.Repository;
using SuperHeroi.Domain.Interfaces.Services;
using SuperHeroi.Domain.Services;
using SuperHeroi.Infra.Data.Context;
using SuperHeroi.Infra.Data.Interfaces;
using SuperHeroi.Infra.Data.Repositories;
using SuperHeroi.Infra.Data.UoW;

namespace SuperHeroi.Infra.CrossCutting.IoC
{
    public class NinjectModulo : NinjectModule
    {
        public override void Load()
        {
            // app
            Bind<IHeroiAppService>().To<HeroiAppService>();
            Bind<IPoderAppService>().To<PoderAppService>();

            // service
            Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            Bind<IHeroiService>().To<HeroiService>();
            Bind<IPoderService>().To<PoderService>();

            // data repos
            Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<,>));
            Bind<IHeroiRepository>().To<HeroiRepository>();
            Bind<IPoderRepository>().To<PoderRepository>();

            // data configs
            Bind(typeof(IContextManager<>)).To(typeof(ContextManager<>));
            Bind<IDbContext>().To<SuperHeroiContext>();
            Bind(typeof(IUnitOfWork<>)).To(typeof(UnitOfWork<>));
        }
    }
}
