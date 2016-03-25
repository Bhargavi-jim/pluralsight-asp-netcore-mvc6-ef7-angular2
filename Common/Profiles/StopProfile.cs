using AutoMapper;
using MyWorld.ViewModels;
using TheWorld.Data.Models;

namespace MyWorld.Common.Profiles
{
    public class StopProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Stop, StopViewModel>().ReverseMap();
        }
    }
}