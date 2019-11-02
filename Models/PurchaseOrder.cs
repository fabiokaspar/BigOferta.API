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
        
        // [ForeignKey("ClientId")]
        // public int ClientId { get; set; }
    
        // public PurchaseOrder(T item)
        // {
        //     this.Add(item);
        // }
        public void confirmPurchaseOrder()
        {
            DateOfPurchase = DateTime.Now;
            
            foreach(T item in this)
            {
                Offer offer = (item as UserOffer).Offer;
                TotalPrice += ((item as UserOffer).Amount) * offer.Price; 
            }
        }

        public void finalizePurchaseOrder()
        {
            if (this.Any())
            {
                this.Clear();
            }

            TotalPrice = 0.0;
        }

    }
}