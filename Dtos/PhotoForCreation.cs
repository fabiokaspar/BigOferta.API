using Microsoft.AspNetCore.Http;

namespace BigOferta.API.Dtos
{
    public class PhotoForCreationDto
    {
        public string Url { get; set; }
        public int Width { get; set; } = 620;
        public int Height { get; set; } = 400;
        public bool IsMain { get; set; } 
        public IFormFile File {get; set; }
        public string PublicId { get; set; }
        public int? OfferId { get; set; }
        public int? UserId { get; set; }
    }
}