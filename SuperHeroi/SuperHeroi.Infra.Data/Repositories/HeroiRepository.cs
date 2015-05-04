using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Domain.Entities;
using SuperHeroi.Domain.Interfaces.Repository;
using SuperHeroi.Infra.Data.Context;

namespace SuperHeroi.Infra.Data.Repositories
{
    public class HeroiRepository : RepositoryBase<Heroi, SuperHeroiContext>, IHeroiRepository
    {
        public IEnumerable<Heroi> ObterHeroiCompleto(Guid id)
        {
            return base.Find(x => x.HeroiId == id);
        }
    }
}
