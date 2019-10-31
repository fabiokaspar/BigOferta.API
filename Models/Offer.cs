using System.Collections.Generic;

namespace BigOferta.API.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Advertiser { get; set; }
        public double Price { get; set; }
        public bool IsHanked { get; set; }
        public virtual ICollection<Photo> Album { get; set; }
        // users que adicionaram a oferta ao seu carrinho
        public virtual ICollection<UserOffer> InterestedUsers { get; set; }
        public virtual ICollection<Message> Feedbacks { get; set; }
    }
}