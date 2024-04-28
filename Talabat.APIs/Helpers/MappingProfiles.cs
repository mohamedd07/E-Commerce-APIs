using AutoMapper;
using Talabat.APIs.DTOS;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTOs>()
                .ForMember(d => d.Brand, o => o.MapFrom(S => S.Brand.Name))
                .ForMember(d => d.Category, o => o.MapFrom(S => S.Category.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<PictureURLResolver>());

            CreateMap<CustomerBasketDTOs, CustomerBasket>();
            CreateMap<BaseketItemDTO, BasketItem>();
            CreateMap<AddressDto,Address>();

         
        }
    }
}
