using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;
using BigOferta.API.Dtos;
using BigOferta.API.Models;

namespace BigOferta.API.Helpers
{
    public static class Utils
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

        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public static void TreatValues(OfferParams offerParams)
        {
            offerParams.Advertiser = offerParams.Advertiser.Trim().RemoveDiacritics().ToLower();
            offerParams.Category = offerParams.Category.Trim().RemoveDiacritics().ToLower();
            offerParams.Description = offerParams.Description.Trim().RemoveDiacritics().ToLower();
            offerParams.Title = offerParams.Title.Trim().RemoveDiacritics().ToLower();
            offerParams.ComoUsar = offerParams.ComoUsar.Trim().RemoveDiacritics().ToLower();
            offerParams.OndeFica = offerParams.OndeFica.Trim().RemoveDiacritics().ToLower();
        }
    }
}