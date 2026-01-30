using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DepoStokBulucu.Data;
using DepoStokBulucu.Models;

namespace DepoStokBulucu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ARAMA SONUCU (Büyük/Küçük harf fark etmez)
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return RedirectToAction("Index");

            // SQL Server varsayýlan olarak "Case Insensitive"dir.
            // Yani "Contains" metodu; "vitra", "Vitra", "VÝTRA" hepsini bulur.
            var product = await _context.Products
                                        .Include(p => p.Location)
                                        .FirstOrDefaultAsync(p => p.Name.Contains(query));

            if (product == null)
            {
                ViewBag.Message = "Aradýðýnýz ürün bulunamadý.";
                return View("Index");
            }

            return View("Result", product);
        }

        // OTOMATÝK TAMAMLAMA API'SÝ (Önerileri Getirir)
        public IActionResult GetProductNames(string term)
        {
            // Kullanýcý "vit" yazdý diyelim.
            // Veritabanýnda içinde "vit" geçen (büyük küçük fark etmeksizin)
            // ilk 10 ürün ismini alýp listeler.
            var products = _context.Products
                                   .Where(p => p.Name.Contains(term))
                                   .Select(p => p.Name)
                                   .Take(10)
                                   .ToList();

            return Json(products);
        }
    }
}