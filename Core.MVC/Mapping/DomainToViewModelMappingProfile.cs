using AutoMapper;
using Core.Domain.Entities;
using Core.MVC.Models;

namespace Core.MVC.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Employee, EmployeeViewModel>();
        }
    }
}
