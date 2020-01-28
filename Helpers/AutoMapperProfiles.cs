using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using BigOferta.API.Dtos;
using BigOferta.API.Models;

namespace BigOferta.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<UserForRegisterDto, User>();

            CreateMap<UserForUpdateDto, User>();
            

            CreateMap<PurchaseOrder<UserOffer>, PurchaseOrderDto>()
                .ForMember(dest => dest.CartOffers, opt => {
                    opt.MapFrom(src => src.ToList());
                });

            CreateMap<PhotoForReturnDto, Photo>().ReverseMap();
            CreateMap<PhotoForCreationDto, Photo>().ReverseMap();
            CreateMap<PhotoForRemovingDto, Photo>().ReverseMap();

            CreateMap<OfferForRegisterDto, Offer>().ReverseMap();
                
            CreateMap<User, UserForReturnDto>().ReverseMap();

            CreateMap<Offer, OfferForCartDto>()
                .ForMember(dest => dest.OfferId, opt => {
                    opt.MapFrom(src => src.Id);
                })
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(photo => photo.IsMain).Url);
                });

            CreateMap<UserOffer, UserOfferForCartDto>().ReverseMap();
            CreateMap<Offer, OfferForReturnDto>().ReverseMap();
        }
    }
}