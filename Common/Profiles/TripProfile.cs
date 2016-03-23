using AutoMapper;
using MyWorld.ViewModels;
using TheWorld.Data.Models;

namespace MyWorld.Common.Profiles
{
    public class TripProfile : Profile
    {
        protected override void Configure() 
        {
            CreateMap<Trip, TripViewModel>().ReverseMap();
        }
    }
}