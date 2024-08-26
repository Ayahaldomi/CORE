using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TASK21_08.Models;
using TASK21_08.NewFolder;

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

        [HttpPost]
        public IActionResult post([FromForm] CategoryPost category)
        {
            if (category.CategoryImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var filePath = Path.Combine(uploadsFolderPath, category.CategoryImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    category.CategoryImage.CopyToAsync(stream);
                }
                
            }
            Category cat = new Category
            {
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage.FileName,
            };
            _db.Categories.Add(cat);
            _db.SaveChanges();

            var response = new
            {
                Success = true,
                Message = $"User {category.CategoryName} created successfully!",
                Code = StatusCodes.Status200OK
            };
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult updateCategory(int id,[FromForm] CategoryPost category)
        {

            if (category.CategoryImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var filePath = Path.Combine(uploadsFolderPath, category.CategoryImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    category.CategoryImage.CopyToAsync(stream);
                }

            }
            var c = _db.Categories.FirstOrDefault(l => l.CategoryId == id);

            c.CategoryName = category.CategoryName ?? c.CategoryName;
            //if (category.CategoryImage == null)
            //{
            //    c.CategoryImage = c.CategoryImage;
            //}
            //else
            //{
            //    c.CategoryImage = category.CategoryImage.FileName;

            //}

            c.CategoryImage = category.CategoryImage.FileName ?? c.CategoryImage;



            _db.Categories.Update(c);
            _db.SaveChanges();

            return Ok();

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
