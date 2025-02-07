using AutoMapper;
using RestFull_Api.Models.Domains;
using RestFull_Api.Models.Domains.DTO;

namespace RestFull_Api.Mapping
{
    public class Mapper :Profile
    {
        public Mapper()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region,CreateRegionDTO>().ReverseMap();
            CreateMap<Region,UpdateDto>().ReverseMap();
            CreateMap<Walk,AddWalkDTO>().ReverseMap();
            CreateMap<Walk,WalkDTO>().ReverseMap();
            CreateMap<Difficulity,DifficulityDTO>().ReverseMap();
            CreateMap<UpdateWalkDTO,Walk>().ReverseMap();
        }
    }
}
