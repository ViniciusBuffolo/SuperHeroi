using Microsoft.Owin;
using Owin;
using SuperHeroi.Infra.CrossCutting.IoC.Security.Startup;
using SuperHeroi.MVC;

[assembly: OwinStartup("StartupConfiguration", typeof(Startup))]
namespace SuperHeroi.MVC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            new IdentityStartup().Configuration(app);
        }
    }
}
