using AutoMapper;
using finjob_backend.Models;
using finjob_backend.Models.DTO;

namespace finjob_backend
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Company, CompanyDTO>()
                .ForMember(x => x.LocationIds, opt => opt.MapFrom(s => s.Locations.Select(l => l.Id).ToList()))
                .ReverseMap();
            CreateMap<Company, CompanyCreateDTO>()
                .ForMember(x => x.LocationIds, opt => opt.MapFrom(s => s.Locations.Select(l => l.Id).ToList()))
                .ReverseMap();
            CreateMap<Company, CompanyUpdateDTO>()
                .ForMember(x => x.LocationIds, opt => opt.MapFrom(s => s.Locations.Select(l => l.Id).ToList()))
                .ReverseMap();

            CreateMap<Job, JobDTO>()
                .ForMember(x => x.LocationIds, opt => opt.MapFrom(s => s.Locations.Select(l => l.Id).ToList()))
                .ForMember(x => x.PositionIds, opt => opt.MapFrom(s => s.Positions.Select(l => l.Id).ToList()))
                .ReverseMap();
            CreateMap<Job, JobCreateDTO>()
                .ForMember(x => x.LocationIds, opt => opt.MapFrom(s => s.Locations.Select(l => l.Id).ToList()))
                .ForMember(x => x.PositionIds, opt => opt.MapFrom(s => s.Positions.Select(l => l.Id).ToList()))
                .ReverseMap();
            CreateMap<Job, JobUpdateDTO>()
                .ForMember(x => x.LocationIds, opt => opt.MapFrom(s => s.Locations.Select(l => l.Id).ToList()))
                .ForMember(x => x.PositionIds, opt => opt.MapFrom(s => s.Positions.Select(l => l.Id).ToList()))
                .ReverseMap();

            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Location, LocationCreateDTO>().ReverseMap();
            CreateMap<Location, LocationUpdateDTO>().ReverseMap();

            CreateMap<Position, PositionDTO>().ReverseMap();
            CreateMap<Position, PositionCreateDTO>().ReverseMap();
            CreateMap<Position, PositionUpdateDTO>().ReverseMap();

            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
        }
    }
}
