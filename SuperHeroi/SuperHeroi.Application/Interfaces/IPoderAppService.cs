using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Application.ViewModels;

namespace SuperHeroi.Application.Interfaces
{
    public interface IPoderAppService : IDisposable
    {
        void Add(PoderViewModel poderViewModel);
        PoderViewModel GetById(Guid id);
        IEnumerable<PoderViewModel> GetAll();
        void Update(PoderViewModel poderViewModel);
        void Remove(PoderViewModel poderViewModel);
    }
}
