using System.ComponentModel.DataAnnotations;
using BigOferta.API.Models;

namespace BigOferta.API.Dtos
{
    public class OfferForCartDto
    {
        public int OfferId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Advertiser { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string PhotoUrl { get; set; }
    }
}