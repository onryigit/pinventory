using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DepoStokBulucu.Data; // Kendi proje isminle aynı olduğundan emin ol
using DepoStokBulucu.Models;

namespace DepoStokBulucu.Controllers
{
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public LocationController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // 1. LİSTELEME EKRANI: Mevcut yüklenenleri görelim
        public async Task<IActionResult> Index()
        {
            var locations = await _context.Locations.ToListAsync();
            return View(locations);
        }

        // 2. YÜKLEME EKRANI: Sadece formu gösterir
        public IActionResult Create()
        {
            return View();
        }

        // 3. KAYDETME İŞLEMİ: Fotoğrafı alıp klasöre atar, veritabanına yazar
        [HttpPost]
        public async Task<IActionResult> Create(Location location, IFormFile? file)
        {
            if (file != null)
            {
                // Resim için benzersiz isim oluştur (çakışma olmasın diye)
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                // Kaydedilecek yer: wwwroot/uploads
                string uploadFolder = Path.Combine(_environment.WebRootPath, "uploads");
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                // Dosyayı fiziksel olarak kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Veritabanına yolunu kaydet
                location.ImagePath = "/uploads/" + uniqueFileName;

                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(location);
        }
        // DETAY SAYFASI: Hem resmi hem de üzerindeki ürünleri getirir
        public async Task<IActionResult> Details(int id)
        {
            // Veritabanından bölgeyi ve İÇİNDEKİ ÜRÜNLERİ (Include) çekiyoruz
            var location = await _context.Locations
                .Include(l => l.Products)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (location == null) return NotFound();

            return View(location);
        }

        // KAYDETME İŞLEMİ: JS'den gelen veriyi kaydeder (Sayfa yenilenmeden)
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product != null)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Json(new { success = true }); // Başarılı mesajı dön
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location != null)
            {
                // Önce bağlı olan ürünleri silelim (Veritabanı hatası almamak için)
                var products = _context.Products.Where(p => p.LocationId == id);
                _context.Products.RemoveRange(products);

                // Sonra bölgeyi silelim
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}