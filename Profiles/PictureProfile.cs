using AutoMapper;
using Shop.Dtos;
using Shop.Models;

namespace Shop.Profiles
{
    public class PictureProfile:Profile
    {
        public PictureProfile()
        {
            CreateMap<Picture, PictureDto>();
            CreateMap<PictureForCreationDto, Picture>();
        }
    }
}
