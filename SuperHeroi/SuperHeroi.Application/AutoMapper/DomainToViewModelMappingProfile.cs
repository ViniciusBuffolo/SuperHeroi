using AutoMapper;
using SuperHeroi.Application.ViewModels;
using SuperHeroi.Domain.Entities;

namespace SuperHeroi.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Heroi, HeroiViewModel>();
            Mapper.CreateMap<Poder, PoderViewModel>();
            Mapper.CreateMap<HeroiPoder, HeroiPoderViewModel>();
        }
    }
}