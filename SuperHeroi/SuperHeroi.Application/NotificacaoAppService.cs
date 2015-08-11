using System;
using System.Collections.Generic;
using AutoMapper;
using SuperHeroi.Application.Interfaces;
using SuperHeroi.Application.ViewModels;
using SuperHeroi.Domain.Entities;
using SuperHeroi.Domain.Interfaces.Services;
using SuperHeroi.Infra.Data.Context;

namespace SuperHeroi.Application
{
    public class NotificacaoAppService: AppServiceBase<SuperHeroiContext>, INotificacaoAppService
    {
        private readonly INotificacaoService _notificacaoService;

        public NotificacaoAppService(INotificacaoService notificacaoService)
        {
            _notificacaoService = notificacaoService;
        }

        public void Add(NotificacaoViewModel notificacaoViewModel)
        {
            var notificacao = Mapper.Map<NotificacaoViewModel, Notificacao>(notificacaoViewModel);
            BeginTransaction();
            _notificacaoService.Add(notificacao);
            Commit();
        }

        public NotificacaoViewModel GetById(Guid id)
        {
            return Mapper.Map<Notificacao, NotificacaoViewModel>(_notificacaoService.GetById(id));
        }

        public IEnumerable<NotificacaoViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Notificacao>, IEnumerable<NotificacaoViewModel>>(_notificacaoService.GetAll());
        }

        public void Update(NotificacaoViewModel notificacaoViewModel)
        {
            var notificacao = Mapper.Map<NotificacaoViewModel, Notificacao>(notificacaoViewModel);
            BeginTransaction();
            _notificacaoService.Update(notificacao);
            Commit();
        }

        public void Remove(NotificacaoViewModel notificacaoViewModel)
        {
            var notificacao = Mapper.Map<NotificacaoViewModel, Notificacao>(notificacaoViewModel);
            BeginTransaction();
            _notificacaoService.Remove(notificacao);
            Commit();
        }
        
        public void Dispose()
        {
            _notificacaoService.Dispose();
        }
    }
}