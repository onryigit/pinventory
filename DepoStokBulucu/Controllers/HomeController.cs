using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DepoStokBulucu.Data;
using DepoStokBulucu.Models;
using FuzzySharp;

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

        // ARAMA SONUCU (Fuzzy Search ile)
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewBag.Message = "Lütfen bir ürün adý girin.";
                return View("Index");
            }

            query = query.ToLower().Trim();

            // Önce tam eþleþme dene (hýzlý)
            var exactMatch = await _context.Products
                .Include(p => p.Location)
                .FirstOrDefaultAsync(p => p.Name.ToLower() == query);

            if (exactMatch != null)
                return View("Result", exactMatch);

            // Bulunamazsa fuzzy search yap
            var allProducts = await _context.Products
                .Include(p => p.Location)
                .ToListAsync();

            var bestMatch = allProducts
                .Select(p => new {
                    Product = p,
                    Score = Fuzz.Ratio(query, p.Name.ToLower())
                })
                .OrderByDescending(x => x.Score)
                .FirstOrDefault();

            // %60+ benzerlik varsa göster
            if (bestMatch != null && bestMatch.Score >= 60)
            {
                return View("Result", bestMatch.Product);
            }

            ViewBag.Message = $"'{query}' için sonuç bulunamadý.";
            return View("Index");
        }

        // OTOMATÝK TAMAMLAMA API'SÝ (Önerileri Getirir)
        public IActionResult GetProductNames(string term)
        {
            var products = _context.Products
                .Where(p => p.Name.Contains(term))
                .Select(p => p.Name)
                .Take(10)
                .ToList();

            return Json(products);
        }
    }
}