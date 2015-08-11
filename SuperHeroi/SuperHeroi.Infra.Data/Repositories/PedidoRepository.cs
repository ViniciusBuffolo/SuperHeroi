using SuperHeroi.Domain.Entities;
using SuperHeroi.Domain.Interfaces.Repository;
using SuperHeroi.Infra.Data.Context;

namespace SuperHeroi.Infra.Data.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido, SuperHeroiContext>, IPedidoRepository
    {

    }
}