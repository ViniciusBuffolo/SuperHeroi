using System;
using System.Collections.Generic;
using SuperHeroi.Application.ViewModels;

namespace SuperHeroi.Application.Interfaces
{
    public interface IPedidoAppService : IDisposable
    {
        void Add(PedidoViewModel pedidoViewModel);
        PedidoViewModel GetById(Guid id);
        IEnumerable<PedidoViewModel> GetAll();
        void Update(PedidoViewModel pedidoViewModel);
        void Remove(PedidoViewModel pedidoViewModel);
    }
}