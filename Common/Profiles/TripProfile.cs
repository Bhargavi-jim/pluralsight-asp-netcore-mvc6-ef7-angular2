using AutoMapper;
using MyWorld.ViewModels;
using MyWorld.Data.Models;

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