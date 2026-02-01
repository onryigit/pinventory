namespace DepoStokBulucu.Models
{
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = new List<Product>();
    }
}