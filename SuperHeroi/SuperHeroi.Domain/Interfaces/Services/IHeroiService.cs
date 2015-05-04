using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Domain.Entities;

namespace SuperHeroi.Domain.Interfaces.Services
{
    public interface IHeroiService : IServiceBase<Heroi>
    {
        IEnumerable<Heroi> ObterHeroiCompleto(Guid id);
    }
}
