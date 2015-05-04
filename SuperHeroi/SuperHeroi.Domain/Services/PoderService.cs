using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHeroi.Domain.Entities;
using SuperHeroi.Domain.Interfaces.Repository;
using SuperHeroi.Domain.Interfaces.Services;

namespace SuperHeroi.Domain.Services
{
    public class PoderService:ServiceBase<Poder>, IPoderService
    {
        private readonly IPoderRepository _poderRepository;

        public PoderService(IPoderRepository poderRepository)
            : base(poderRepository)
        {
            _poderRepository = poderRepository;
        }
    }
}
