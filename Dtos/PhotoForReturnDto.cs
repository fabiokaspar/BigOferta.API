namespace BigOferta.API.Dtos
{
    public class PhotoForReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; } 
        public string PublicId { get; set; }
        public int? OfferId { get; set; }
        public int? UserId { get; set; }
    }
}