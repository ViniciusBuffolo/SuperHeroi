using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SuperHeroi.Application.Interfaces;
using SuperHeroi.Application.ViewModels;
using SuperHeroi.Domain.Entities;
using SuperHeroi.Domain.Interfaces.Services;
using SuperHeroi.Infra.Data.Context;

namespace SuperHeroi.Application
{
    public class PoderAppService : AppServiceBase<SuperHeroiContext>, IPoderAppService
    {
        private readonly IPoderService _poderService;

        public PoderAppService(IPoderService poderService)
        {
            _poderService = poderService;
        }

        public void Add(ViewModels.PoderViewModel poderViewModel)
        {
            var poder = Mapper.Map<PoderViewModel, Poder>(poderViewModel);
            BeginTransaction();
            _poderService.Add(poder);
            Commit();
        }

        public ViewModels.PoderViewModel GetById(Guid id)
        {
            return Mapper.Map<Poder, PoderViewModel>(_poderService.GetById(id));
        }

        public IEnumerable<ViewModels.PoderViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Poder>, IEnumerable<PoderViewModel>>(_poderService.GetAll());
        }

        public void Update(ViewModels.PoderViewModel poderViewModel)
        {
            var poder = Mapper.Map<PoderViewModel, Poder>(poderViewModel);
            BeginTransaction();
            _poderService.Update(poder);
            Commit();
        }

        public void Remove(ViewModels.PoderViewModel poderViewModel)
        {
            var poder = Mapper.Map<PoderViewModel, Poder>(poderViewModel);
            BeginTransaction();
            _poderService.Remove(poder);
            Commit();
        }

        public void Dispose()
        {
            _poderService.Dispose();
        }
    }
}
