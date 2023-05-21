using AutoMapper;
using Movie_Catalog_REST_API.Models;

namespace Movie_Catalog_REST_API.Dto
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap();
        }
    }
}
