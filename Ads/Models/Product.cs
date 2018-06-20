namespace Ads.Controllers
{
    public class Product
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public float OldPrice { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
    }
}