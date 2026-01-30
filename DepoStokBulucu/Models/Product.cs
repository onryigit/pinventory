namespace DepoStokBulucu.Models
{
    public class Product
    {
        public int Id { get; set; }

        // Ürün adı: "Vitra S20 Klozet"
        public string Name { get; set; } = string.Empty;

        // Koordinatlar (Yüzdelik olarak tutacağız, örn: 45.5)
        // int yerine double kullanıyoruz ki hassas konum olsun.
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }

        // Hangi Bölge/Fotoğraf içinde? (Foreign Key)
        public int LocationId { get; set; }
        public Location? Location { get; set; }
    }
}