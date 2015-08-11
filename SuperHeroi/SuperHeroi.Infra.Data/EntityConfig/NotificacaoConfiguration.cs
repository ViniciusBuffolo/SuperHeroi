using System.Data.Entity.ModelConfiguration;
using SuperHeroi.Domain.Entities;

namespace SuperHeroi.Infra.Data.EntityConfig
{
    public class NotificacaoConfiguration : EntityTypeConfiguration<Notificacao>
    {
        public NotificacaoConfiguration()
        {
            HasKey(c => c.NotificacaoId);
        }
    }
}
