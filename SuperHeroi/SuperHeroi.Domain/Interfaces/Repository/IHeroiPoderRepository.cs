using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Domain.Entities;

namespace SuperHeroi.Domain.Interfaces.Repository
{
    public interface IHeroiPoderRepository : IRepositoryBase<HeroiPoder>
    {
        IEnumerable<HeroiPoder> BuscarPoderPorIdHeroi(Guid idHeroi);
    }
}
