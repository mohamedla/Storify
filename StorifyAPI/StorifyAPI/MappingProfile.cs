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

            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
