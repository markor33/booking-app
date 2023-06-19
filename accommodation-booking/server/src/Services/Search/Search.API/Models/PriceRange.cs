namespace Search.API.Models
{
    public class PriceRange
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public PriceRange(int minPrice, int maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }
    }
}
