using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperHeroi.Application.ViewModels
{
    public class PedidoViewModel
    {
        public PedidoViewModel()
        {
            PedidoId = Guid.NewGuid();
            NotificacoesViewModel = new List<NotificacaoViewModel>();
        }

        [Key]
        public Guid PedidoId { get; set; }
        public Guid UsuarioId { get; set; }
        public string CodRef { get; set; }
        public string CodAssinatura { get; set; }
        public string CodTransacao { get; set; }
        public bool FlgAtivo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }

        public virtual ICollection<NotificacaoViewModel> NotificacoesViewModel { get; set; }
    }
}