using AutoMapper;
using finjob_backend.Models;
using finjob_backend.Models.DTO;

namespace finjob_backend
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Company, CompanyCreateDTO>().ReverseMap();
            CreateMap<Company, CompanyUpdateDTO>().ReverseMap();
        }
    }
}
