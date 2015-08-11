using SuperHeroi.Domain.Entities;
using SuperHeroi.Domain.Interfaces.Repository;
using SuperHeroi.Infra.Data.Context;

namespace SuperHeroi.Infra.Data.Repositories
{
    public class NotificacaoRepository: RepositoryBase<Notificacao, SuperHeroiContext>, INotificacaoRepository
    {
         
    }
}