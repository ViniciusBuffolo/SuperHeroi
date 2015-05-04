﻿using AutoMapper;
using SuperHeroi.Application.ViewModels;
using SuperHeroi.Domain.Entities;

namespace SuperHeroi.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<HeroiViewModel, Heroi>();
            Mapper.CreateMap<PoderViewModel, Poder>();
        }
    }
}