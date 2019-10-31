using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BigOferta.API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Posted { get; set; }

        [ForeignKey("SenderId")]
        public int SenderId { get; set; }
        public virtual User Sender { get; set; }
       
        [ForeignKey("OfferId")]
        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; }
    }
}