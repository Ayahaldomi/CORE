using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TASK21_08.DTOs;
using TASK21_08.Models;

namespace TASK21_08.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDbContext _db;
        public ProductsController(MyDbContext db) { _db = db; }

        [HttpGet]
        public IActionResult Get()
        {
            var product = _db.Products.ToList();
            return Ok(product);
        }

        [HttpPost]
        public IActionResult postProduct([FromForm] productPost product)
        {
            if (product.ProductImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var filePath = Path.Combine(uploadsFolderPath, product.ProductImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    product.ProductImage.CopyToAsync(stream);
                }

            }

            var pro = new Product
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ProductImage = product.ProductImage.FileName
            };

            _db.Products.Add(pro);
            _db.SaveChanges();
            return Ok(pro);
        }

        [HttpPut("{id}")]
        public IActionResult putProduct(int id, [FromForm] productPost product)
        {
            if (product.ProductImage != null)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var filePath = Path.Combine(uploadsFolderPath, product.ProductImage.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    product.ProductImage.CopyToAsync(stream);
                }

            }
            var x = _db.Products.FirstOrDefault(l => l.ProductId == id);


            x.ProductName = product.ProductName;
                x.Description = product.Description;
                x.Price = product.Price;
                x.CategoryId = product.CategoryId;
                x.ProductImage = product.ProductImage.FileName;
           
            //_db.Products.Update(x);
            
            _db.SaveChanges();
            return Ok(x);
        }

        [Route("sort")]
        [HttpGet]
        public IActionResult sort()
        {

            var product = _db.Products.OrderByDescending(l => l.Price).ToList();



            return Ok(product);
        }

        [HttpGet("{id:int:max(10)}")]
        public IActionResult GetById(int id) { 
            if (id <= 0)
            {

            return BadRequest(); 
            }
            
            var product = _db.Products.Where(l => l.ProductId == id).FirstOrDefault();

            return Ok(product);
        }

        [HttpGet("/ProductById/{id}")]
        public IActionResult GetByProductId(int id)
        {
            if (id == null)
            {
                var products = _db.Products.ToList();
                return BadRequest(products);
            }

            var product = _db.Products.Where(l => l.ProductId == id).ToList();

            return Ok(product);
        }

        [HttpGet("/ProductByCategoryId/{id}")]
        public IActionResult GetByCategoryId(int id)
        {
            if (id == null)
            {
                var products = _db.Products.ToList();
                return BadRequest(products);
            }

            var product = _db.Products.Where(l => l.CategoryId == id).ToList();

            return Ok(product);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name) {
            if (name == null) { 
                return BadRequest();
            }
            var productName = _db.Products.Where(l => l.ProductName == name);
            if (productName == null) {
                return NotFound();


            }
            return Ok(productName);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var product = _db.Products.Where(l => l.ProductId == id).FirstOrDefault();

            var deleteOrders = _db.Orders.Where(l => l.UserId == id).ToList();
            foreach (var order in deleteOrders)
            {

                _db.Orders.Remove(order);
            }

            _db.Products.Remove(product);
            _db.SaveChanges();
            return Ok();
        }



    }
}
