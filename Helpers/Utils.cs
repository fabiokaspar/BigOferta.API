using System.Collections.Generic;
using AutoMapper;
using BigOferta.API.Dtos;
using BigOferta.API.Models;

namespace BigOferta.API.Helpers
{
    public class Utils
    {
        
        public static List<OfferForCartDto> GetOfferCartDtoList(
            List<UserOffer> userOffers, 
            IMapper mapper)
        {
            List<OfferForCartDto> offerForCartDtos = new List<OfferForCartDto>();

            userOffers.ForEach(uo => {
                OfferForCartDto offerDto = mapper.Map<OfferForCartDto>(uo.Offer);
                offerDto.Amount = uo.Amount;

                offerForCartDtos.Add(offerDto);
            });

            return offerForCartDtos;
        }
    }
}