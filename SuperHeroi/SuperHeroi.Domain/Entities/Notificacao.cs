using System;

namespace SuperHeroi.Domain.Entities
{
    public class Notificacao
    {
        public Notificacao()
        {
            NotificacaoId = Guid.NewGuid();
        }

        public Guid NotificacaoId { get; set; }
        public string TipoNotificacao { get; set; }
        public string CodRef { get; set; }
        public string CodAssinatura { get; set; }
        public string CodTransacao { get; set; }
        public string CodNotificacao { get; set; }
        public string Status { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool FlgChecado { get; set; }

        public Guid PedidoId { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
