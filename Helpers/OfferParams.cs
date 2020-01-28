namespace BigOferta.API.Helpers
{
    public class OfferParams
    {
        // private const double minPrice = 10.0;
        // private const double maxPrice = 20000.0;
        // private double price = 100.0;
        
        // public double Price { 
        //     get { return price; } 
        //     set { price = (value > maxPrice) ? maxPrice : ((value < minPrice) ? minPrice : value); }
        // }

        public string QueryFilter { get; set; }
        public string OrderBy { get; set; } = "Title";
        public string Title { get; set; }
        public string Category { get; set; }
        public string ComoUsar { get; set; }
        public string OndeFica { get; set; }
        public string Description { get; set; }
        public string Advertiser { get; set; }
        public bool IsHanked { get; set; } = false;
        public int OfferId { get; set; } = 0;
    }
}