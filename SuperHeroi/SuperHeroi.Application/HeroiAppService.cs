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
    public class HeroiAppService : AppServiceBase<SuperHeroiContext>, IHeroiAppService
    {
        private readonly IHeroiService _heroiService;

        public HeroiAppService(IHeroiService heroiService)
        {
            _heroiService = heroiService;
        }

        public void Add(HeroiViewModel heroiViewModel)
        {
            var heroi = Mapper.Map<HeroiViewModel, Heroi>(heroiViewModel);

            foreach (var itemPoder in heroiViewModel.Poderes)
            {
                var obj = new Poder()
                {
                    PoderId = itemPoder.PoderId,
                    Descricao = itemPoder.Descricao
                };
                heroi.PoderList.Add(obj);
            }

            BeginTransaction();
            _heroiService.Add(heroi);
            Commit();
        }

        public HeroiViewModel GetById(Guid id)
        {
            return Mapper.Map<Heroi, HeroiViewModel>(_heroiService.GetById(id));
        }

        public IEnumerable<HeroiViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<Heroi>, IEnumerable<HeroiViewModel>>(_heroiService.GetAll());
        }

        public void Update(HeroiViewModel heroiViewModel)
        {
            var heroi = Mapper.Map<HeroiViewModel, Heroi>(heroiViewModel);
            BeginTransaction();
            _heroiService.Update(heroi);
            Commit();
        }

        public void Remove(HeroiViewModel heroiViewModel)
        {
            var heroi = Mapper.Map<HeroiViewModel, Heroi>(heroiViewModel);
            BeginTransaction();
            _heroiService.Remove(heroi);
            Commit();
        }

        public IEnumerable<HeroiViewModel> ObterHeroiCompleto(Guid id)
        {
            return Mapper.Map<IEnumerable<Heroi>, IEnumerable<HeroiViewModel>>(_heroiService.ObterHeroiCompleto(id));
        }

        public void Dispose()
        {
            _heroiService.Dispose();
        }
    }
}
