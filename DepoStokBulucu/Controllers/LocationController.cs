using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DepoStokBulucu.Data;
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

        public async Task<IActionResult> Index()
        {
            var locations = await _context.Locations.ToListAsync();
            return View(locations);
        }

      
        public IActionResult Create()
        {
            return View();
        }

  
        [HttpPost]
        public async Task<IActionResult> Create(Location location, IFormFile? file)
        {
            if (file != null)
            {
          
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                string uploadFolder = Path.Combine(_environment.WebRootPath, "uploads");
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

         
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

        
                location.ImagePath = "/uploads/" + uniqueFileName;

                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(location);
        }
    
        public async Task<IActionResult> Details(int id)
        {
           
            var location = await _context.Locations
                .Include(l => l.Products)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (location == null) return NotFound();

            return View(location);
        }

      
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product != null)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Json(new { success = true }); 
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location != null)
            {
               
                var products = _context.Products.Where(p => p.LocationId == id);
                _context.Products.RemoveRange(products);

               
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {

                int locationId = product.LocationId;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

          
                return RedirectToAction("Details", new { id = locationId });
            }

            return RedirectToAction("Index");
        }
  
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
         
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = product.LocationId });
            }
            return View(product);
        }
    }
}