using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task19_8.Models;

namespace task19_8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MyDbContext _db;

        public ProductController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _db.Products.ToList();
            return Ok(categories);
        }

        [HttpGet("id")]
        public IActionResult getCAtegoryById(int id)
        {
            var category = _db.Products.FirstOrDefault(l => l.ProductId == id);
            return Ok(category);
        }
    }
}
