using SuperHeroi.Infra.CrossCutting.IoC.Security.Manager;

namespace SuperHeroi.Infra.CrossCutting.IoC.Security
{
    public class IdentityManager : IIdentityManager
    {
        private ApplicationSignInManager _signInManager = null;
        private ApplicationUserManager _appUserManager = null;

        public IdentityManager(ApplicationSignInManager signInManager, ApplicationUserManager appUserManager)
        {
            _signInManager = signInManager;
            _appUserManager = appUserManager;
        }

        public void Teste()
        {
            
        }
    }
}