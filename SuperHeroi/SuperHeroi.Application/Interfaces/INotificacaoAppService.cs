using System;
using System.Collections.Generic;
using SuperHeroi.Application.ViewModels;

namespace SuperHeroi.Application.Interfaces
{
    public interface INotificacaoAppService : IDisposable
    {
        void Add(NotificacaoViewModel notificacaoViewModel);
        NotificacaoViewModel GetById(Guid id);
        IEnumerable<NotificacaoViewModel> GetAll();
        void Update(NotificacaoViewModel notificacaoViewModel);
        void Remove(NotificacaoViewModel notificacaoViewModel);
    }
}