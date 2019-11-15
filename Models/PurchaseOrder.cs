using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigOferta.API.Models
{
    public class PurchaseOrder<T> : List<T> 
    {
        // T = UserOffer
        public int Id { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public double TotalPrice { get; set; } = 0.0;
        public virtual User Client { get; set; }
        
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }


        public void confirmPurchaseOrder()
        {
            DateOfPurchase = DateTime.Now;
            TotalPrice = 0.0;

            foreach(T item in this)
            {
                Offer offer = (item as UserOffer).Offer;
                TotalPrice += ((item as UserOffer).Amount) * offer.Price; 
            }
        }

        public void finalizePurchaseOrder()
        {
            if (this.Count > 0)
            {
                this.RemoveAll(item => (item as UserOffer).UserId == ClientId);
            }
        }

    }
}