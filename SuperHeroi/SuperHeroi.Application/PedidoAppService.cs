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
    public class PedidoAppService : AppServiceBase<SuperHeroiContext>, IPedidoAppService
    {
        private readonly IPedidoService _pedidoService;

        public PedidoAppService(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        public void Add(PedidoViewModel pedidoViewModel)
        {
            var pedido = Mapper.Map<PedidoViewModel, Pedido>(pedidoViewModel);
            BeginTransaction();
            _pedidoService.Add(pedido);
            Commit();
        }

        public PedidoViewModel GetById(Guid id)
        {
            return Mapper.Map<Pedido, PedidoViewModel>(_pedidoService.GetById(id));
        }

        public IEnumerable<PedidoViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoViewModel>>(_pedidoService.GetAll());
        }

        public void Update(PedidoViewModel pedidoViewModel)
        {
            var pedido = Mapper.Map<PedidoViewModel, Pedido>(pedidoViewModel);
            BeginTransaction();
            _pedidoService.Update(pedido);
            Commit();
        }

        public void Remove(PedidoViewModel pedidoViewModel)
        {
            var pedido = Mapper.Map<PedidoViewModel, Pedido>(pedidoViewModel);
            BeginTransaction();
            _pedidoService.Remove(pedido);
            Commit();
        }

        public void Dispose()
        {
            _pedidoService.Dispose();
        }
    }
}