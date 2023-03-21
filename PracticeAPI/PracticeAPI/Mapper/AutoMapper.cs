using AutoMapper;
using PracticeAPI.Models;

namespace PracticeAPI.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<BrandDto, Brand>();
        }
    }
}
