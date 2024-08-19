using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task19_8.Models;

namespace task19_8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CategoriesController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _db.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("id")]
        public IActionResult getCAtegoryById(int id)
        {
            var category = _db.Categories.FirstOrDefault(l => l.CategoryId == id);
            return Ok(category);
        }

    }
}
    

