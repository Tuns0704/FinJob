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

            CreateMap<Job, JobDTO>().ReverseMap();
            CreateMap<Job, JobCreateDTO>().ReverseMap();
            CreateMap<Job, JobUpdateDTO>().ReverseMap();

            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Location, LocationCreateDTO>().ReverseMap();
            CreateMap<Location, LocationUpdateDTO>().ReverseMap();

            CreateMap<Position, PositionDTO>().ReverseMap();
            CreateMap<Position, PositionCreateDTO>().ReverseMap();
            CreateMap<Position, PositionUpdateDTO>().ReverseMap();
        }
    }
}
