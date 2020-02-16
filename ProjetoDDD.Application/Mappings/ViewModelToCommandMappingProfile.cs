using AutoMapper;
using ProjetoDDD.Application.ViewModels;
using ProjetoDDD.Domain.Commands;

namespace ProjetoDDD.Application.Mappings
{
    public class ViewModelToCommandMappingProfile : Profile
    {
        public ViewModelToCommandMappingProfile()
        {
            CreateMap<DemoViewModel, AddDemoCommand>();
            CreateMap<DemoViewModel, UpdateDemoCommand>();
            CreateMap<DemoViewModel, RemoveDemoCommand>();
        }
    }
}