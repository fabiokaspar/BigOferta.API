using System.Collections.Generic;

namespace BigOferta.API.Dtos
{
    public class OfferForRegisterDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Advertiser { get; set; }
        public double Price { get; set; }
        public bool IsHanked { get; set; }
        public string ComoUsar { get; set; }
        public string OndeFica { get; set; }
        public virtual ICollection<PhotoForReturnDto> Photos { get; set; }
    }
}