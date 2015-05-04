using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Application.ViewModels;

namespace SuperHeroi.Application.Interfaces
{
    public interface IHeroiAppService : IDisposable
    {
        void Add(HeroiViewModel heroiViewModel);
        HeroiViewModel GetById(Guid id);
        IEnumerable<HeroiViewModel> GetAll();
        void Update(HeroiViewModel heroiViewModel);
        void Remove(HeroiViewModel heroiViewModel);
        IEnumerable<HeroiViewModel> ObterHeroiCompleto(Guid id);
    }
}
