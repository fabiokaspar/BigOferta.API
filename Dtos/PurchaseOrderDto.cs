using System;
using System.Collections.Generic;

namespace BigOferta.API.Dtos
{
    public class PurchaseOrderDto
    {
        public int Id { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public double TotalPrice { get; set; }
        public int ClientId { get; set; }
        public virtual List<UserOfferForCartDto> CartOffers { get; set; }
    }
}