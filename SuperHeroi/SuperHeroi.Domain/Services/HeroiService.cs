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
    public class HeroiService : ServiceBase<Heroi>, IHeroiService
    {
        private readonly IHeroiRepository _heroiRepository;

        public HeroiService(IHeroiRepository heroiRepository)
            : base(heroiRepository)
        {
            _heroiRepository = heroiRepository;
        }

        public IEnumerable<Heroi> ObterHeroiCompleto(Guid id)
        {
            return _heroiRepository.ObterHeroiCompleto(id);
        }
    }
}
