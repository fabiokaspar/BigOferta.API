using System.ComponentModel.DataAnnotations.Schema;

namespace BigOferta.API.Models
{
    public class UserOffer
    {
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        [ForeignKey("OfferId")]
        public int OfferId { get; set; }
        public virtual Offer Offer { get; set; }
        public int Amount { get; set; }
    }
}