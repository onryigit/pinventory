namespace DepoStokBulucu.Models
{
    public class Location
    {
        public int Id { get; set; }

        // Bölge adı: "A Blok Giriş", "Hırdavat Reyonu Sol" gibi
        public string Name { get; set; } = string.Empty;

        // Fotoğrafın dosya yolu: "/img/depo_giris.jpg" gibi
        public string ImagePath { get; set; } = string.Empty;

        // Bu bölgedeki ürünlerin listesi (İlişki)
        public List<Product> Products { get; set; } = new List<Product>();
    }
}