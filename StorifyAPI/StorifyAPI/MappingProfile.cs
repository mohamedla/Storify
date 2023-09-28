using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace StorifyAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Store, StoreDTO>();

            CreateMap<StoreCreateDTO, Store>();

            CreateMap<StoreUpdateDTO, Store>();

            CreateMap<Employee, EmployeeDTO>();

            CreateMap<EmployeeCreateDTO, Employee>();

            CreateMap<EmployeeUpdateDTO, Employee>().ReverseMap();
        }
    }
}
