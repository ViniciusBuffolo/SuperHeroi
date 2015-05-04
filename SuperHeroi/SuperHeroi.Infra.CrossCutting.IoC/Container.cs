using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator.NinjectAdapter.Unofficial;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace SuperHeroi.Infra.CrossCutting.IoC
{
    public class Container
    {
        public Container()
        {
            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(GetModule()));
        }

        public StandardKernel GetModule()
        {
            return new StandardKernel(new NinjectModulo());
        }
    }
}
