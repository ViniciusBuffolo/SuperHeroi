using System;
using System.Collections.Generic;

namespace SuperHeroi.Domain.Entities
{
    public class Pedido
    {
        public Pedido()
        {
            PedidoId = Guid.NewGuid();
            Notificacoes = new List<Notificacao>();
        }

        public Guid PedidoId { get; set; }
        public Guid UsuarioId { get; set; }
        public string CodRef { get; set; }
        public string CodAssinatura { get; set; }
        public string CodTransacao { get; set; }
        public bool FlgAtivo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }

        public virtual ICollection<Notificacao> Notificacoes { get; set; }
    }
}
