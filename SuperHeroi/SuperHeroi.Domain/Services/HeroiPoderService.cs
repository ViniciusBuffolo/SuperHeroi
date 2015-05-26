using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Domain.Entities;
using SuperHeroi.Domain.Interfaces.Repository;
using SuperHeroi.Domain.Interfaces.Services;

namespace SuperHeroi.Domain.Services
{
    public class HeroiPoderService : ServiceBase<HeroiPoder>, IHeroiPoderService
    {
        private readonly IHeroiPoderRepository _heroiPoderRepository;

        public HeroiPoderService(IHeroiPoderRepository heroiPoderRepository)
            : base(heroiPoderRepository)
        {
            _heroiPoderRepository = heroiPoderRepository;
        }

        public IEnumerable<HeroiPoder> BuscarPoderPorIdHeroi(Guid idHeroi)
        {
            return _heroiPoderRepository.BuscarPoderPorIdHeroi(idHeroi);
        }
    }
}
