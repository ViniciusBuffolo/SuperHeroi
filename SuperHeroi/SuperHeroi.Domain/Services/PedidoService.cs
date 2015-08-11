using SuperHeroi.Domain.Entities;
using SuperHeroi.Domain.Interfaces.Repository;
using SuperHeroi.Domain.Interfaces.Services;

namespace SuperHeroi.Domain.Services
{
    public class PedidoService:ServiceBase<Pedido>, IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
            : base(pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
    }
}