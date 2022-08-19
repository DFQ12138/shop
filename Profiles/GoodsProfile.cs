using AutoMapper;
using Shop.Dtos;
using Shop.Models;

namespace Shop.Profiles
{
    public class GoodsProfile:Profile
    {
        public GoodsProfile()
        {
            CreateMap<Goods, GoodsDto>()
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => src.OriginalPrice * (decimal?)(src.DiscountPresent ?? 1))
                );
            CreateMap<GoodsForCreationDto, Goods>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                );

        }
    }
}
