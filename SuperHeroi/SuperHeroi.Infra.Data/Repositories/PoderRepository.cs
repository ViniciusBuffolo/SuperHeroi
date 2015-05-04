using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Domain.Entities;
using SuperHeroi.Domain.Interfaces.Repository;
using SuperHeroi.Infra.Data.Context;
using SuperHeroi.Infra.Data.Repositories;

namespace SuperHeroi.Infra.Data.Repositories
{
    public class PoderRepository : RepositoryBase<Poder, SuperHeroiContext>, IPoderRepository
    {
    }
}
