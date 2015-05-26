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
    public class HeroiPoderRepository : RepositoryBase<HeroiPoder, SuperHeroiContext>, IHeroiPoderRepository
    {

        public IEnumerable<HeroiPoder> BuscarPoderPorIdHeroi(Guid idHeroi)
        {
            return base.Find(x => x.HeroiId == idHeroi);
        }
    }
}
