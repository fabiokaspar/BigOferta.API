using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigOferta.API.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public string Street { get; set; }
        public string Neighbourhood { get; set; }
        public string HomeNumber { get; set; }
        public string AddressComplement { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime OrderDate { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}