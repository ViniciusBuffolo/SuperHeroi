using System.Data.Entity.ModelConfiguration;
using SuperHeroi.Domain.Entities;

namespace SuperHeroi.Infra.Data.EntityConfig
{
    public class PedidoConfiguration : EntityTypeConfiguration<Pedido>
    {
        public PedidoConfiguration()
        {
            HasKey(c => c.PedidoId);
        }
    }
}