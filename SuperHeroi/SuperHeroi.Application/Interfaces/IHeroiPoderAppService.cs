using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Application.ViewModels;

namespace SuperHeroi.Application.Interfaces
{
    public interface IHeroiPoderAppService : IDisposable
    {
        void Add(HeroiPoderViewModel heroiPoderViewModel);
        HeroiPoderViewModel GetById(Guid id);
        IEnumerable<HeroiPoderViewModel> GetAll();
        void Update(HeroiPoderViewModel heroiPoderViewModel);
        void Remove(HeroiPoderViewModel heroiPoderViewModel);
        IEnumerable<HeroiPoderViewModel> BuscarPoderPorIdHeroi(Guid idHeroi);
    }
}
