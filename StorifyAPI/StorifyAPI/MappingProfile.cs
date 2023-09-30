using AutoMapper;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.Material;
using Entities.Models;
using Entities.Models.Material;

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

            CreateMap<MaterialType, MaterialTypeDTO>();
            CreateMap<MaterialTypeCreateDTO, MaterialType>();
            CreateMap<MaterialTypeUpdateDTO, MaterialType>();

            CreateMap<MaterialGroup, MaterialGroupDTO>();
            CreateMap<MaterialGroupCreateDTO, MaterialGroup>();
            CreateMap<MaterialGroupUpdateDTO, MaterialGroup>();

            CreateMap<MaterialItem, MaterialItemDTO>();
            CreateMap<MaterialItemCreateDTO, MaterialItem>();
            CreateMap<MaterialItemUpdateDTO, MaterialItem>();

            CreateMap<MaterialUnit, MaterialUnitDTO>();
            CreateMap<MaterialUnitCreateDTO, MaterialUnit>();
            CreateMap<MaterialUnitUpdateDTO, MaterialUnit>();
        }
    }
}
