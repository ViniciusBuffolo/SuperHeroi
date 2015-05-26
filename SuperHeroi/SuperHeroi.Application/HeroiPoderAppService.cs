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
    public class HeroiPoderAppService : AppServiceBase<SuperHeroiContext>, IHeroiPoderAppService
    {
        private readonly IHeroiPoderService _heroiPoderService;

        public HeroiPoderAppService(IHeroiPoderService heroiPoderService)
        {
            _heroiPoderService = heroiPoderService;
        }

        public void Add(HeroiPoderViewModel heroiPoderViewModel)
        {
            var heroi = Mapper.Map<HeroiPoderViewModel, HeroiPoder>(heroiPoderViewModel);

            //foreach (var itemPoder in heroiViewModel.Poderes)
            //{
            //    var obj = new Poder()
            //    {
            //        PoderId = itemPoder.PoderId,
            //        Descricao = itemPoder.Descricao
            //    };
            //    heroi.PoderList.Add(obj);
            //}

            BeginTransaction();
            _heroiPoderService.Add(heroi);
            Commit();
        }

        public HeroiPoderViewModel GetById(Guid id)
        {
            return Mapper.Map<HeroiPoder, HeroiPoderViewModel>(_heroiPoderService.GetById(id));
        }

        public IEnumerable<HeroiPoderViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<HeroiPoder>, IEnumerable<HeroiPoderViewModel>>(_heroiPoderService.GetAll());
        }

        public void Update(HeroiPoderViewModel heroiPoderViewModel)
        {
            var heroi = Mapper.Map<HeroiPoderViewModel, HeroiPoder>(heroiPoderViewModel);
            BeginTransaction();
            _heroiPoderService.Update(heroi);
            Commit();
        }

        public void Remove(HeroiPoderViewModel heroiPoderViewModel)
        {
            var heroi = Mapper.Map<HeroiPoderViewModel, HeroiPoder>(heroiPoderViewModel);
            BeginTransaction();
            _heroiPoderService.Remove(heroi);
            Commit();
        }

        public IEnumerable<HeroiPoderViewModel> BuscarPoderPorIdHeroi(Guid idHeroi)
        {
            return Mapper.Map<IEnumerable<HeroiPoder>, IEnumerable<HeroiPoderViewModel>>(_heroiPoderService.BuscarPoderPorIdHeroi(idHeroi));
        }

        public void Dispose()
        {
            _heroiPoderService.Dispose();
        }
    }
}
