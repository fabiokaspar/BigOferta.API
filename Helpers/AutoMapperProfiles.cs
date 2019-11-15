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
                // .ForMember(dest => dest.TotalPrice, opt => {
                //     opt.MapFrom(src => src.TotalPrice);
                // })
                // .ForMember(dest => dest.DateOfPurchase, opt => {
                //     opt.MapFrom(src => src.DateOfPurchase);
                // })
                // .ForMember(dest => dest.Id, opt => {
                //     opt.MapFrom(src => src.Id);
                // });

            CreateMap<User, UserForReturnDto>();
                // .ForMember(dest => dest.Purchase, opt => {
                //     opt.MapFrom(src => src.Id);
                // })
                // .ForMember(dest => dest.Purchase.ClientId, opt => {
                //     opt.MapFrom(src => src.Id);
                // })
                // .ForMember(dest => dest.Purchase.CartOffers, opt => {
                //     opt.MapFrom(src => src.CartOffers);
                // });

            CreateMap<Offer, OfferForCartDto>()
                .ForMember(dest => dest.OfferId, opt => {
                    opt.MapFrom(src => src.Id);
                })
                .ForMember(dest => dest.PhotoUrl, opt => {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(photo => photo.IsMain).Url);
                });
            CreateMap<UserOffer, UserOfferForCartDto>().ReverseMap();
            CreateMap<Offer, OfferForReturnDto>().ReverseMap();

            // CreateMap<List<Offer>, List<OfferForCartDto>>();
        }
    }
}