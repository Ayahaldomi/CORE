using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TASK21_08.Models;

namespace TASK21_08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyDbContext _db;

        public CategoryController(MyDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get() { 

            var category = _db.Categories.ToList(); 
            return Ok(category);
        }

        [Route("getById/{id:int:min(5)}")]
        [HttpGet]
        public IActionResult getById(int id) {
            if (id <=0)
            {
                return BadRequest();
            }
            var categoryId = _db.Categories.FirstOrDefault(l => l.CategoryId == id);
            
            return Ok(categoryId);
        }

        [Route("getByName")]
        [HttpGet]
        public IActionResult GetByName(string? name) {
            if (name == null)
            {
                return BadRequest();
            }
            var categoryName = _db.Categories.FirstOrDefault(l => l.CategoryName == name);
            if (categoryName == null) {

                return BadRequest();
            }

            return Ok(categoryName);
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteById(int id) {
            if (id <= 0)
            {
                return BadRequest();
            }
            var deleteorder = _db.Orders.Where(l => l.Product.CategoryId == id).ToList();

            _db.Orders.RemoveRange(deleteorder);
            _db.SaveChanges();

            var deleteproduct = _db.Products.Where(l => l.CategoryId == id).ToList();
            

                _db.Products.RemoveRange(deleteproduct);
                _db.SaveChanges();

            
            var deleteCategory = _db.Categories.FirstOrDefault(c => c.CategoryId == id);

            _db.Categories.Remove(deleteCategory);
            _db.SaveChanges();
            return NoContent();
        }

    }
}
