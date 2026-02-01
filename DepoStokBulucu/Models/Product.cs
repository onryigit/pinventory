namespace DepoStokBulucu.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }

        public int LocationId { get; set; }
        public Location? Location { get; set; }
    }
}