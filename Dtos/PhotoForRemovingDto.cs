namespace BigOferta.API.Dtos
{
    public class PhotoForRemovingDto
    {
        public int Id { get; set; }
        public string PublicId { get; set; }
        public int? OfferId { get; set; }
        public int? UserId { get; set; }
    }
}