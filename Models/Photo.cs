using System.ComponentModel.DataAnnotations.Schema;

namespace BigOferta.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; } 
        public string PublicId { get; set; }
        
        public virtual User User { get; set; }
        public virtual Offer Offer { get; set; }

        [ForeignKey("OfferId")]
        public int? OfferId { get; set; }
        
        [ForeignKey("PhotoId")]
        public int? UserId { get; set; }
    }
}