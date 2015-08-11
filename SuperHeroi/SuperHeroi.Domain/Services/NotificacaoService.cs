using SuperHeroi.Domain.Entities;
using SuperHeroi.Domain.Interfaces.Repository;
using SuperHeroi.Domain.Interfaces.Services;

namespace SuperHeroi.Domain.Services
{
    public class NotificacaoService: ServiceBase<Notificacao>, INotificacaoService
    {
        private readonly INotificacaoRepository _notificacaoRepository;

        public NotificacaoService(INotificacaoRepository notificacaoRepository)
            : base(notificacaoRepository)
        {
            _notificacaoRepository = notificacaoRepository;
        }
    }
}