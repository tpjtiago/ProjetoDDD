using AutoMapper;
using ProjetoDDD.Application.ViewModels;
using ProjetoDDD.Domain.Models;

namespace ProjetoDDD.Application.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<DemoModel, DemoViewModel>();
        }
    }
}